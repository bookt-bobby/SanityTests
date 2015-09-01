// public vars  
   dates = []; pos = [];
var emps = [];
compid = 9; var res;
mode = "edit";
var selPos;
var strTo;
var allShifts = [];
var fc;
var oTable;

function fixScheduleHeader() {


  oTable = $('#empTbl').dataTable({
            "bJQueryUI": true,
            "sScrollY": "400px",
            "sScrollX": "100%",
            "bScrollCollapse": true,
            "bPaginate": false,
            //"bSort": false,
            "bSearchable":false
            
        });
    fc = new FixedColumns(oTable);
}

function clearMsg() {
    addErr("");
}
function refreshCell(eid, d) {
    var c = $("li[data-Date='" + d + "'][data-EmpId='" + eid + "']").parent().parent();
    getShifts(eid, d, function (content) {
        var str = content.join('');
        c.html(str);
    });
}
function refreshShifts(){
    $(".shift-cell").each(
        function (key) {
            if ($($(".shift-cell")[key]).html().indexOf("selected") > -1) {
                getShifts($(this).attr('data-EmpId'), $(this).attr("data-Date"), function (content) {
                    var str = content.join('');
                    $($(".shift-cell")[key]).html(str);
                    $($(".shift-cell")[key]).find(".shift-list2").addClass('selected');
                });
                
            }
           
       }
   );
}

// initialize
function addErr(errStr) {
    $("#errMsg").show();
    $("#errMsg").html(errStr);
    if (errStr == '') {
        $("#errMsg").hide();
    }
}
function empsAdded() {
    
    addSelectableClass();
    
    $(".fancyActions").click(function () { addErr(""); });
    $(".fancyActions").fancybox({ onStart: ensureFancy });
    fixScheduleHeader();
    $(".selectable").click(clearMsg);
    updatePosViewingLink();
    //initSlider();   
}
function ensureFancy(link) {
    if (link[0].id == "btnSched") {
        if ($(".ui-selected").length == 0) { addErr("Please select at least one time slot");return false;}
        if ($(".shift-list-item-scheduled.ui-selected").length > 0) { addErr("Cannot schedule shift for time slot");return false;}
    }
    if (link[0].id == "btnUnsched") {
        if ($(".avail-list-item.ui-selected").length > 0) { addErr("No shift scheduled for one or more selected time slot "); return false; }
        if ($(".shift-list-item-scheduled.ui-selected").length == 0) { addErr("Please select a shift to unschedule"); return false; }
    }

}
$(function () {
   
    if (window.location.href.indexOf("m=0") > -1)
        mode = "view";
    addErr('');
    $(".from").button();
    $(".to").button();
  
    $.get('/Handlers/ScheduleHandler.ashx?&method=get&entity=position&mode=' + mode, function (data, textStatus) {   
        pos = data.result;
        buildUpTable();
        pushPositionList();
        
    });
   
});
function getSelPos() {
    var selPos = readCookie('selPos');
    if (selPos == null  && pos.length>0)
        selPos = pos[0].name;
    else if (selPos == null)
        return '<a href="http://google.com">Add Position</a>'
    return selPos;
}
/* create employee table */
function buildUpTable() {
    var strTo = getToDate();
    var strFrom = getFromDate();
    $('#divSchedule').empty();
        
    //create main employee table
    $('<table />', {
        'class': 'display selectable scroll-content',
        id:'empTbl'
    }).appendTo('#divSchedule');
    $('<thead />', {
        id: 'eTH'
    }).appendTo('#empTbl');
   
    $('<tbody />', {
        id: 'eTB'
    }).appendTo('#empTbl');
   
    $("#empTbl").attr("cellspacing", "0");
    $("#empTbl").attr("cellpadding", "0");
    bindToFromDates();
  
  
    $.get('/Handlers/ScheduleHandler.ashx?start=' + encodeURI(strFrom) + '&end=' + encodeURI(strTo) + '&method=get&entity=DatesOfOperation&mode=' + mode, function (data, textStatus) {
        if (!data.success)
            alert(data.errorMsg);
        dates = data.result;
        pushDateRows();
        pushEmpRows();
        addShiftListItems();
    });
}
/*bind "To and From" */
function bindToFromDates() {
    $(".from").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 2,
        changeYear: true,
        dateFormat:"mm/dd/yy",
        onSelect: function (selectedDate) {
            
            $(".to").datepicker("option", "minDate", selectedDate);
            var selDate=$.datepicker.parseDate('mm/dd/yy', selectedDate);
            var maxDate =selDate;            
            maxDate.setDate(maxDate.getDate()+7)
            //$(".to").datepicker("option", "maxDate", maxDate);
            var curToDate = $.datepicker.parseDate('mm/dd/yy', $(".to").val());
            //var curToDate = $.datepicker.formatDate('mm/dd/yy', strToDate);
            if (curToDate > maxDate)
                curToDate = maxDate;
            else if (curToDate <= selDate)
                curToDate = maxDate;
            var strNewDate=$.datepicker.formatDate('mm/dd/yy', curToDate)
            $(".to").val(strNewDate);
            $(this).addClass('ui-state-active');
            createCookie("from", selectedDate, 21);
            createCookie("to", strNewDate, 21);
           
            buildUpTable();
        }
    });
    $(".to").datepicker({
        defaultDate: "+1w",
        numberOfMonths: 2,
        dateFormat: "mm/dd/yy",
        onSelect: function (selectedDate) {
           
            $(this).addClass('ui-state-active');
            createCookie("to", selectedDate, 21);
            buildUpTable();
        }
    });

    $(".from").val(getFromDate());
    $(".to").val(getToDate());
    
    if(hasCookie('from'))
        $('.from').addClass('ui-state-active');
    if (hasCookie('to'))
        $('.to').addClass('ui-state-active');
     
     

}
function hasCookie(ck) { 
    var strTo = readCookie(ck);
    if (strTo == null || strTo == '')
        return false;
    else
        return true
}
function getToDate() {
    var strTo = readCookie("to");
    if (strTo == null || strTo == '')
        strTo = getTodayDate(7);
    return strTo;
}
function getFromDate() {
    var strFrom = readCookie("from");
    if (strFrom == null || strFrom == '')
        strFrom = getTodayDate(0);
    return strFrom;
}
function getTodayDate(i) {
    var d = new Date();
    d.setDate(d.getDate() + i);
    return d.getMonth() +
        "/" + d.getDate() +
        "/" + d.getFullYear();
};
/* bind positions */
function pushPositionList() {
    /*position list*/
    $('<ul/>', {
        id: 'olPos',
    }).appendTo('#divChangePos');

    if (pos.length > 0) {
        /* pushes positions with checked obtained from cookie */
        $.each(pos, function (key, val) {
            var items = [];
            var selected = '';
            var selPos = getSelPos();
            if (selPos != null && selPos.indexOf(val.name) > -1)
                selected = 'checked';
            items.push("<input type='radio' onclick='updateSelPos();' name='pos' class='cbPos' " + selected + " value='" + val.name + "' id='check" + key + "' /><label for='check" + key + "'>" + val.name + "</label>");
            $('<li/>', {
                'class': 'list-item',
                html: items.join('')
            }).appendTo('#olPos');
        });
    }
    else {
        alert('no positions returned');
    }
       

    $('#olPos').buttonset();
    $("#olShifts").buttonset();
}
var selEmps = [];
function postShift(empId,shiftId,d,callback) {
    $.post("/Handlers/ScheduleHandler.ashx?date=" + d + "&empid=" + empId + "&shiftId=" + shiftId + "&method=set&entity=shift&position=" + getSelPos() + '&mode=' + mode, function (data, textStatus) {
        callback();
    });
}
var msg;
function removeShift(eid,sid,d,callback) {
    $.post("/Handlers/ScheduleHandler.ashx?date=" + d + "&empid=" + eid + "&shiftId=" + sid + "&method=delete&entity=shift&position=" + getSelPos() + '&mode=' + mode, function (data, textStatus) {
        callback();
    });
   
}
function formShiftsRemovedMsg(unEmps,unDays,totShifts) {
     showMsg(totShifts + " shifts unscheduled for " + unEmps + " employees spanning " + unDays + " days"); 
}
function undoAction() {
    while (undoCall.length > 0) {
        (undoCall.pop())();
    }   
}
var undoCall = [];
function unScheduleShift() {
    var totShifts = 0;
    $("#empTbl .ui-selected").each(function (key) {
        var d = $(this).attr("data-Date");
        var eid = $(this).attr("data-EmpId");
        var sid = $(this).attr("data-ShiftId");

        removeShift(eid, sid, d, function () {
            refreshCell(eid, d);
            undoCall.push(function () {
                postShift(eid, sid, d,
                    function () { refreshCell(eid, d) }
                );
            });
           
        });
        totShifts += 1;
    });
    msg = totShifts + " shifts unscheduled <small> <a href='#' onclick='undoAction()' >Undo</a></small>";
    undoCall.unshift(function () { showMsg("") });
    showMsg(msg);
}
function scheduleShift() {
    var msg = '';
    var uniqueDays = [];
    var uniqueEmps = [];
    var days = [];
    var shiftId = -1;
    selEmps = [];
    var time;
    $("#olShifts .ui-state-active").each(
        function (key) {
            shiftId = $(this).attr("data-ShiftId");
            time = $(this).attr("data-Start") + " - " + $(this).attr("data-End");
        });
    //undoCall.push(function(){});
    var totalShifts=0;
    $("#empTbl .ui-selected").each(function(key){
        var d=$(this).attr("data-Date");
        var eId = $(this).attr("data-EmpId");
        selEmps.push(eId);
        days.push(d);
        totalShifts+=1;
        postShift(eId, shiftId, d, function () {
            var c = $("li[data-Date='" + d + "'][data-EmpId='" + eId + "']").parent().parent();
            refreshCell(eId,d);
            undoCall.push(function () { removeShift(eId, shiftId, d, function () { refreshCell(eId,d) }) });
        });
        //for messaging
        if (uniqueDays.indexOf(d) == -1)
            uniqueDays.push(d);
        if (uniqueEmps.indexOf(eId) == -1)
            uniqueEmps.push(eId);
    })
    var strDates=" across " + uniqueDays.length + " days";
    if (uniqueDays.length==1){
        strDates=" on " + uniqueDays[0];
    }
    
    msg = uniqueEmps.length + " employee(s) scheduled for " + time + strDates + "<small> <a href='#' onclick='undoAction()' >Undo</a></small>";
    undoCall.unshift(function () { showMsg("") });
    showMsg(msg);
}
/* bind shifts*/
function showMsg(msg) {
    $("#infoMsg").html(msg);
    $("#infoMsg").show();
}
function addShiftListItems() {
    selPos = getSelPos();
    $.get('/Handlers/ScheduleHandler.ashx?&method=get&entity=shift&position=' + selPos + '&mode=' + mode, function (data, textStatus) {
        if (data.success)
            shifts = data.result;
        else
            alert(data.errorMsg);
        pushShiftList(shifts);
    });
}
function pushShiftList(shifts) {

    /* create new shift list item ul or empty existing */
    if (document.getElementById('olShifts') == null) {
        $('<ul/>', {
            id: 'olShifts',
            'class': 'shift-list'
        }).appendTo('#divScheduleShift');
    }
    else {
        $('#olShifts').empty();
    }

    /* appends shifts html with checked obtained from cookie */
    $.each(shifts, function (key, val) {
        var items = [];
        var selected = '';
        var selPos=getSelPos();
        if ( selPos != null && selPos.indexOf(val.job.position.name) > -1)
            selected = 'checked3';

        items.push("<input type='radio' name='rad'  onclick='$.fancybox.close();scheduleShift();" +  "' id='radShift" + key + "' /><label data-shiftId='" + val.id + "' data-Start='" +  val.strStartTime + "' data-End='" +  val.strEndTime + "' for='radShift" + key + "'>" + val.job.position.name + ' (' + val.strStartTime + '-' + val.strEndTime + ")</label>");
        $('<li/>', {
            'class': 'list-item',
            html: items.join('')
        }).appendTo('#olShifts');
    });



    $("#olShifts").buttonset();
}   
function updateSelPos() {
    updateSelPosCookie();
    buildUpTable();

    
}
/* updates cookie */
function updateSelPosCookie() {
    items = [];
    $('.cbPos').each(function (index) {
        if($(this).attr('checked'))
            items.push($(this).val());
    });
    createCookie('selPos', items.join(','), 21);
    $.fancybox.close();
}
/* shows/hides based on cookie set above - not being used right now*/
function updatePosViewingLink() {
    var selPos = getSelPos();
    if (selPos != null && selPos != '') {
        $('#aChangePositions').html("<span class='ui-button-text' >" + selPos + "</span>");
        if(hasCookie('selPos'))
            $('#aChangePositions').addClass('ui-state-active');
    }
}
function addSelectableClass() {
    $('.selectable').bind("mousedown", function (e) {
        e.metaKey = true;
    });

    $(".selectable").selectable({
        filter: ".shift-list-item",
        unselected: function () {
            clearMsg();
            $(".ui-state-highlight", this).each(function () {
                $(this).removeClass('ui-state-highlight');
            });
            $(".ui-selected", this).each(function () {
                $(this).addClass('ui-state-highlight');
            });
        },
        selected: function () {
            clearMsg();
            $(".ui-selected", this).each(function () {
                $(this).addClass('ui-state-highlight');
            });
        }
    });


    $('li').filter('.shift-list-item').hover(
         function () {
             $(this).addClass('ui-state-hover');
         },
         function () {
             $(this).removeClass('ui-state-hover');
         }
);
}

/* cookie functions*/
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    createCookie(name, "", -1);
}

/* Building up table functions */
function pushDateRows() {
    items = [];
    items.push("<th class='actions emp-cell'><button href='#divChangePos' class='fancyActions' id='aChangePositions' >" + getSelPos() + "</button></th>");
    $.each(dates, function (key, val) {
        items.push("<th id='col" + key + "'  ><ol class='shift-list-item'><li>" + val + "</li></ol></th>");
    });

    $('<tr/>', {
        'class': 'date-row',
        html: items.join('')
    }).appendTo('#eTH');

    $("input:submit, a, button", ".actions").button();
}
var numCells;
function pushEmpRows() {

    $.get('/Handlers/ScheduleHandler.ashx?&strDate=' + getFromDate() + '&endDate=' + getToDate() + '&method=get&entity=employee&mode=' + mode, function (data, textStatus) {
        if (!data.success) {
            alert(data.errorMsg);
        }
        emps = data.result;
        var display = "none";
        var lastCame;
        numCells = emps.length  * dates.length;
        cellsCompleted = 0;
        $.each(emps, function (key, emp) {
            $('<tr/>', {
                //'class':  (key % 2 == 0 ? "even" : "odd"),
                //style: 'display:none',
                id: emp.id
            }).appendTo('#eTB');
            
            $('<td/>', {
                'class': 'emp-cell',
                html: emp.fullName
            }).appendTo('#' + emp.id);
            isLastRow = false;

            if (key == emps.length - 1)
                isLastRow = true;

            $.each(dates, function (k, d) {
                isLast = false;
                if (k == dates.length - 1 && isLastRow)
                    isLast = true;
                pushShifts(emp.id, d, emp.id, isLast, k);
            });
            if (selPos == null || selPos.indexOf(pos.name) > -1) {
                $('#' + emp.id).css('display', '');

            }

        });

    });
}
var cellsCompleted;
function pushShifts(empId, d, rowid, isLast, key) {
    getShifts(empId, d, function (result) {
        $('<td/>', {
            'class': 'shift-cell',
            html: result.join('')
        }).attr({
            "data-EmpId": empId,
            "data-Date": d
        }).appendTo('#' + rowid);
        cellsCompleted += 1;
        if (cellsCompleted == numCells) {
            empsAdded();
        }
    });
}
function getShifts(empId, d,callback) {
    return $.get('/Handlers/ScheduleHandler.ashx?date=' + d + '&method=get&entity=block&compid=' + compid + '&empid=' + empId + "&position=" + getSelPos() + "&mode=" + mode, function (data, textStatus) {
        var shifts = [];
        var items=[];
        if (!data.success)
            alert(data.errorMsg);
        blocks = data.result;
        allShifts.push(blocks);
        items.push("<ol class='shift-list2 ' >");
        for (i = 0; i < blocks.length; i++) {
            var block = blocks[i];
            var strTime = block.strStart + '-' + block.strEnd;
            switch (block.type) {
                case null: break;
                case "morn": items.push("<li data-empId='" + empId + "' data-Date='" + d + "' class='ui-state-default shift-list-item avail-list-item' >&nbsp;</li>");
                    break;
                case "night": items.push("<li data-empId='" + empId + "' data-Date='" + d + "' class=' ui-state-default shift-list-item avail-list-item'  >&nbsp;</li>");
                    break;
                case "shift":
                    items.push("<li data-empId='" + empId + "' data-Date='" + d + "' data-ShiftId='" + block.shift.id + "' class=' ui-state-default shift-list-item shift-list-item-scheduled" + block.shift.job.position.name.toLowerCase() + "' >" + strTime + "</li>");
            }

        }
        if (items.length == 0) {//error 
            items.push("<li class='ui-state-default shift-list-item avail-list-item' >&nbsp;</li>");
            items.push("<li class='ui-state-default shift-list-item avail-list-item' >&nbsp;</li>");
        }
        items.push('</ol>');
        callback(items);
    });
  
}
function setWidth() {
    
    $('.page').width(.85 * $(window).width());

    var tblWidth = $('.shift-tbl').width();
    listWidth = (tblWidth - 100) / (dates.length+2);
    $(".shift-list2").width(listWidth);
}
