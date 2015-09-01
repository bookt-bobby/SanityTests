'use strict';
var app = angular.module('app', ['ngRoute']).config(['$routeProvider',
function ($routeProvider, $location) {
        $routeProvider.
        when('/editTests', {
            templateUrl: '/templates/edit-tests.html',
            controller: 'TestsController',
            title: 'Edit Tests'
        }).
        when('/showTests', {
            templateUrl: '/templates/show-tests.html',
            controller: 'TestsController',
            title: 'Show tests'
        }).
            when('/runTests', {
                templateUrl: '/templates/run-tests.html',
                controller: 'TestsController',
                title: 'Run tests'
            }).
            when('/run/:suiteId', {
                templateUrl: '/templates/run-tests.html',
                controller: 'TestsController',
                title: 'Run Suite'
            }).
            when('/edit/:suiteId', {
                templateUrl: '/templates/edit-tests.html',
                controller: 'TestsController',
                title: 'Edit Suite'
            }).
            when('/viewSuites', {
                templateUrl: '/templates/view-suites.html',
                controller: 'SuitesController',
               title: 'View Suites - Sanity Tests'
            }).
            when('/editSuites', {
                templateUrl: '/templates/edit-suites.html',
                controller: 'SuitesController',
                title: 'Edit Suites - Sanity Tests'
            }).
        otherwise({
           // redirectTo: '/tests/new/'
        });
  }]);

app.run(function ($window,$http, $rootScope, $route) {
    $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded; charset=utf-8";
    $rootScope.$on("$routeChangeSuccess", function (event, currentRoute, previousRoute) {
        //Change page title, based on Route information
        $rootScope.title = $route.current.title;
        if ($route.current.title){       
            $window.document.title = $route.current.title;
        }
        r = $rootScope;

    });
});
var r;
function editRow(button) {
    var id = $(button).attr('data-id');
    if ($(button).val() == 'edit') {
        $(".view-" + id).hide();
        $(".edit-" + id).show();
        $(button).val('view');
    }
    else {
        $(".edit-" + id).hide();
        $(".view-" + id).show();
        $(button).val('edit');
    }

}
var initialized = false;

function SuitesController($scope, $http, $location, $routeParams) {
    $http.get('/ws/?method=get&entity=suite').success(function (data) {
        if (!data) { alert("No data"); }
        var s = data.result;
        for (var i = 0; i < s.length; i++) {
            for (var i = 0; i < s.length; i++) {
                s[i].Selected = false;
            }
        }
        if (s.length > 0) { }
        $scope.suites = s;
    });

    $http.get('/ws/?method=get&entity=company').success(function (data) {
        if (!data) { alert("No data"); }
        $scope.c = data.result;
    });

    $scope.validateSuites = function () {
        $($scope.suites).each(function (j) {
            if (this.Suite.TestInOrder) {
                return $scope.validateSync(this.Tests,this.Suite);
            }
            else
                return $scope.validateAsync(this.Tests,this.Suite);
        });

    };
    $scope.unPauseTest = function (t) {
        t.IsPaused = false;
    }
    $scope.saveCompany = function () {
        $http.post('/ws/?method=save&entity=company', JSON.stringify($scope.c)).success(function (data) {
            if (!data) { alert("No data"); }
            $scope.c = data.result;
        });
    }
    $scope.validateAsync = function (tests, suite) {
        suite.success = true;
        $(tests).each(function (i) {
            var t = this;
            t.TestSuccess = '';
            t.ActualValue = '';
            if (t.Url == null || t.Url == '' || t.Url.indexOf('http') == -1) {
                t.ActualValue = 'NA';
                t.TestSuccess = 'Not run';
                t.TestResultIcon = 'icon-ban'
                return;//equivalent of continue in each()
            }
            if (t.PauseFirst) {
                t.IsPaused = true;
                while (t.IsPaused) {
                   pausecomp(100);//loop untill unPaused
                }
                alert('unpaused');
            }

            try {
                if (t.Method != null && t.Method.toLowerCase() == 'post')
                    $http.post(t.Url, t.PostData).success(function (data) {
                        validateTest(data, t);
                        if (t.TestSuccess == 'Passed')
                            suite.success = true && suite.success;
                        else {
                            alert(t.Test + ' failed');
                            suite.success = false;
                        }
                        console.log(t.Test + ' completed');
                    });
                else {
                    $http.get(t.Url).success(function (data) {
                        validateTest(data, t);
                        if (t.TestSuccess == 'Passed')
                            suite.success = true && suite.success;
                        else {
                            alert(t.Test + ' failed');
                            suite.success = false;

                        }
                        console.log(t.Test + ' completed');
                    });
                }
            }
            catch (e) {
                console.log(e);
            }
        });
    }
    $scope.validateSync = function (ts, suite) {
        suite.success = true;
        recVal(ts, 0);
        function recVal(tests, i) {
            if (i >= tests.length) return;
            var t = tests[i];
            t.TestSuccess = '';
            t.ActualValue = '';
            if (t.Url == null || t.Url == '' || t.Url.indexOf('http') == -1) {
                t.ActualValue = 'NA';
                t.TestSuccess = 'Not run';
                t.TestResultIcon = 'icon-ban'
                i += 1;
                recVal(tests, i);
                //return;//equivalent of continue in each()
            }
            else {
                if (t.PauseFirst) {
                    alert('pausing');
                    t.IsPaused = true;
                    while (t.IsPaused) {
                        ;//loop untill unPaused
                    }
                    alert('unpaused');
                }
                try {
                    if (t.Method != null && t.Method.toLowerCase() == 'post') {
                        $http.post(t.Url, t.PostData).success(function (data) {
                            validateTest(data, t);
                            if (t.TestSuccess == 'Passed')
                                suite.success = true && suite.success;
                            else {
                                alert(t.Test + ' failed');
                                suite.success = false;

                            }
                            i += 1;
                            console.log(t.Test + ' finished');
                            recVal(tests, i);
                        }
                        );
                    }
                    else {
                        $http.get(t.Url).success(function (data) {
                            validateTest(data, t);
                            if (t.TestSuccess == 'Passed')
                                suite.success = true && suite.success;
                            else {
                                alert(t.Test + ' failed');
                                suite.success = false;
                            }
                                
                            i += 1;
                            console.log(t.Test + ' finished')
                            recVal(tests, i);
                        });
                    }
                }
                catch (e) {
                    console.log(e);
                }
            }
        }

    }
    $scope.delete = function () {
        if (confirm('Are you sure you would like to delete the selected record(s)?')) {
            var lastRowSel = 0;
            var suites= $scope.suites;
            $(suites).each(function (i) {
                if (this.Selected)
                    lastRowSel = i;
            });

            var successCount = 0;
            $(suites).each(function (i) {
                if (this.Selected) {
                    var id = this.Suite.Id
                    var request = $http.post('/ws/?entity=suite&method=delete&id=' + id);
                    request.success(function (data) {
                        if (data.success) {
                            successCount += 1;
                        } else {
                            $scope.displayMsg('Failed to delete suite.  Msg was : ' + data.result, true);
                        }
                        if (i == lastRowSel) {
                            $scope.displayMsg('Successfully deleted ' + successCount + ' tests', false);
                            $scope.reloadRoute($location);
                        }
                    });
                }
            });

        }
    };
    $scope.save = function () {
        console.log(this);
        //var isNew = (this.t.Test.Id == null || this.t.Test.Id == '');
        this.s.AddedOn = '';
        var request = $http.post('/ws/?entity=suite&method=save', JSON.stringify(this.s));
        request.success(function (data) {
            if (data.success) {
               // if (!isNew) {
                    $scope.displayMsg('Successfully saved suite', false);

                //}
                //else {
                  //  $scope.displayMsg('Successfully saved new test', false);
                   // $scope.reloadRoute($location);
                //}


            } else {
                $scope.displayMsg("Failed to save test.  Msg retured was: " + data.result, true)
            }
        });
    };
    $scope.displayMsg = function (text, isError) {
        if (isError) {

            $('#successMsg').html('');
            $('#errorMsg').text(text);
            $('#errorMsg').show('slow');
            $('#errorMsg').fadeOut(3000);
        } else {
            $('#errorMsg').show();
            $('#errorMsg').html('');
            $('#successMsg').text(text);
            $('#successMsg').show('slow');
            $('#successMsg').fadeOut(3000);
        }
    }//end scope declarations
    $scope.reloadRoute = function ($location) {
        var c = parseInt($location.search().s);
        if (isNaN(c)) c = 0;
        var newUrl = window.location.href.replace('?s=' + c.toString(), '').replace('&s=' + c.toString(), '');
        c = c + 1;
        if (newUrl.indexOf('?') == -1) newUrl += '?'; else newUrl += '&';
        newUrl = newUrl + 's=' + (c);
        window.location.href = newUrl;

    }

}

function TestsController($window,$rootScope, $scope, $http, $location, $routeParams) {
    //$window.document.title = $rootScope.title;
    $scope.suiteId = $routeParams.suiteId;
    $http.get('/ws/?method=get&entity=test&get&suiteSortOrder=' + $scope.suiteId).success(function (data) {
        if (!data) { alert("No data");}
        var t = data.result;
        for (var i = 0; i < t.length; i++) {
            t[i].Selected = false;
            t[i].TestResultIcon = '';
        }
        
        if (t.length > 0 && $scope.suiteId) {
            $scope.suite = t[0].Suite;
        } else if (t.length > 0) {
            $scope.suite = 'All Tests';
        }
        $scope.tests = t;
    });

    $http.get('/ws/?method=get&entity=company').success(function (data) {
        if (!data) { alert("No data"); }
        $scope.c = data.result;
    });

    $scope.saveCompany = function () {
        $http.post('/ws/?method=save&entity=company',JSON.stringify($scope.c)).success(function (data) {
            if (!data) { alert("No data"); }
            $scope.c = data.result;
        });
    }

    angular.element('#example').ready(function () {
        //if(!initialized) setTimeout(initTable,700);
        //initialized=true;
    });

    $scope.delete = function () {
        if (confirm('Are you sure you would like to delete the selected record(s)?')) {
            var lastRowSel = 0;
            var tests = $scope.tests;
            $(tests).each(function (i) {
                if (this.Selected)
                    lastRowSel = i;
            });
            var successCount=0;
            $(tests).each(function (i) {
                console.log(this);
                if (this.Selected) {
                    var id = this.Test.Id  
                    var request = $http.post('/ws/?entity=test&method=delete&id=' + id);
                    request.success(function (data) {
                        
                        if (data.success) {
                            successCount += 1;
                            //$scope.displayMsg('Successfully deleted test' + this.Test, false);
                        } else {
                            $scope.displayMsg('Failed to delete test.  Msg was : ' + data.result, true);
                        }
                        if (i == lastRowSel) {
                            $scope.displayMsg('Successfully deleted ' + successCount + ' tests', false);
                            $scope.reloadRoute($location);
                        }
                    });
                }
            });
            
        }
    };
    $scope.reloadRoute = function ($location) {
        var c = parseInt($location.search().s);
        if (isNaN(c)) c = 0;
        var newUrl = window.location.href.replace('?s=' + c.toString(), '').replace('&s=' + c.toString(),'');
        c = c + 1;
        if (newUrl.indexOf('?') == -1) newUrl += '?'; else newUrl +='&';
        newUrl = newUrl + 's=' + (c) ;
        window.location.href = newUrl;
        
    }
    $scope.save = function () {
        var isNew = (this.t.Test.Id == null || this.t.Test.Id == '');
        var request=$http.post('/ws/?entity=test&method=save', JSON.stringify(this.t));
        request.success(function (data) {
            if (data.success) {
                if (!isNew){
                    $scope.displayMsg('Successfully saved test', false);
                    
            }
            else {
                    $scope.displayMsg('Successfully saved new test', false);
                    $scope.reloadRoute($location);
            }


            } else {
                $scope.displayMsg("Failed to save test.  Msg retured was: " + data.result,true)
            }
        });
    };
    $scope.currTest = 0;
    $scope.unPauseTest = function (t) {
        t.IsPaused = false;
        $scope.recVal($scope.tests, $scope.currTest);
    }

    $scope.validate = function () {
        if ($scope.suite.TestInOrder) {
            return $scope.validateSync();
        }
        else
            return $scope.validateAsync();
    };
    $scope.validateAsync = function () {
        var tests = $scope.tests;
        console.log(tests);
        $(tests).each(function (i) {
            var t = this.Test;
            t.TestSuccess = '';
            t.ActualValue = '';
            if (t.Url == null || t.Url == '' || t.Url.indexOf('http') == -1) {
                t.ActualValue = 'NA';
                t.TestSuccess = 'Not run';
                t.TestResultIcon = 'icon-ban'
                return;//equivalent of continue in each()
            }
            try{
                if (t.Method != null && t.Method.toLowerCase() == 'post')
                    $http.post(t.Url, t.PostData).success(function (data) {
                        validateTest(data, t);
                        console.log(t.Test + ' completed');
                    });
                else {
                    $http.get(t.Url).success(function (data) {
                        validateTest(data, t);
                        console.log(t.Test + ' completed');
                    });
                } 
            }
            catch (e) {
                console.log(e);
            }
        });
    }
    $scope.validateSync = function () {
        var ts = $scope.tests;
        console.log(ts);
        $scope.recVal(ts,0);
      

    }
    $scope.recVal=function recVal(tests, i) {
        if (i >= tests.length) return;
        var t = tests[i].Test;
            t.TestSuccess = '';
            t.ActualValue = '';
            if (t.Url.indexOf('http') == -1) {
                if ($scope.c && $scope.c.BaseUrl != null && $scope.c.BaseUrl != '') {
                    t.Url = $scope.c.BaseUrl + t.Url;
                }
            }

           //if bad then no go
            if (t.Url == null || t.Url == '' || t.Url.indexOf('http') == -1) {
                t.ActualValue = 'NA';
                t.TestSuccess = 'Not run';
                t.TestResultIcon = 'icon-ban'
                i += 1;
                recVal(tests, i);
                //return;//equivalent of continue in each()
            }
            else {
                for (var property in retParams) {
                    if (retParams.hasOwnProperty(property)) {
                        // do stuff
                        console.log("Property: " + property);
                        var val=retParams[property]
                        console.log("Val: " + val);
                        var exp="\{\{" + property + "\}\}";
                        t.Url = t.Url.replace(new RegExp(exp, 'g'), val);
                        t.PostData = t.PostData.replace(new RegExp(exp, 'g'), val);
                        t.Value = t.Value.replace(new RegExp(exp, 'g'), val);
                        console.log("Url: " + t.Url);
                    }
                }
                try {
                    if (t.Method != null && t.Method.toLowerCase() == 'post') {
                        $http.post(t.Url, t.PostData).success(function (data) {
                            validateTest(data, t);
                            if (t.IsPaused) {
                                $scope.currTest = i;
                                return true;
                            }
                            i += 1;
                            console.log(t.Test + ' finished');
                            if (i < tests.length) {
                                var nextTest=tests[i].Test
                                if (nextTest.PauseFirst) {
                                    nextTest.IsPaused = true;//alert('Paused for ' + t.Test + '. Please press ok when you are ready to proceed');
                                    $scope.currTest = i;
                                    //alert('should be taking a break')
                                    return true;
                                } else {
                                     $scope.recVal(tests, i);
                                }
                            }
                           
                           
                        }
                        );
                    }
                    else {
                        $http.get(t.Url).success(function (data) {
                            validateTest(data, t);
                            i += 1;
                            console.log(t.Test + ' finished')
                            if (i < tests.length) {
                                var nextTest = tests[i].Test
                                if (nextTest.PauseFirst) {
                                    nextTest.IsPaused = true;//alert('Paused for ' + t.Test + '. Please press ok when you are ready to proceed');
                                    $scope.callback = recVal(tests, i);
                                    return true;
                                } else {
                                    recVal(tests, i);
                                }
                            }
                        });
                    }
                }
                catch (e) {
                    console.log(e);
                }
            }
        }  
    $scope.displayMsg = function (text, isError) {
        if (isError) {

            $('#successMsg').html('');
            $('#errorMsg').text(text);
            $('#errorMsg').show('slow');
            $('#errorMsg').fadeOut(3000);
        } else {
            $('#errorMsg').show();
            $('#errorMsg').html('');
            $('#successMsg').text(text);
            $('#successMsg').show('slow');
            $('#successMsg').fadeOut(3000);
        }
}//end scope declarations

};
function pausecomp(millis) {
    var date = new Date();
    var curDate = null;
    do { curDate = new Date(); }
    while (curDate - date < millis);
}
var retParams = new Object();
function validateTest(data, test) {
   // console.log("Validating Test.." + test.Test)
    // console.log(data);
    pausecomp(1000);
    console.log(test);
    test.Response = JSON.stringify(data);
    try {

        var ft = deepFind(data, test.Property);
       
        test.ActualValue = ft;
        if (ft == test.Value) {
            test.TestSuccess = 'Passed';
            test.ErrorFromPath = 'NA'
            test.TestResultIcon = 'icon-ok';
            if (test.ReturnParamName) {
                var rpVal = deepFind(data, test.ReturnParamPath);
                retParams[test.ReturnParamName] = rpVal;
            }
        }
        else {
            test.IsPaused = true;
            test.TestSuccess = 'Failed';
            test.TestResultIcon = ' icon-exclamation-sign';
            var o = deepFind(data, test.ErrorMessagePath);
            if (o) {
                test.ErrorFromPath = o.toString();
            }
        }
    } catch (e) {//keep trying error paths
        test.TestSuccess = 'Failed';
        test.TestResultIcon = 'icon-exclamation-sign';
        try {
            var errmsg = test.ErrorMessagePath.split(',');
            //console.log(errmsg)
            for (var i = 0; i < errmsg.length; i++) {
                try {
                    var o = deepFind(data, errmsg[i]);
                    if (o) {
                        test.ErrorFromPath = o.toString();
                    }
                } catch (e) { };
            }
        } catch (e) { }
    }
}
function deepFind(obj, path) {
    if (path == null) return;
    for (var i = 0, path = path.split('.'), len = path.length; i < len; i++) {
        if ($.isArray(obj[path[i]])) {
            obj = obj[path[i]][path[i + 1]];
            i++;

        } else {
            obj = obj[path[i]];
        }

    };
    return obj;
};

/*
* Example initialisation
*///$(document).ready(initTable);
var fc;
var oTable;
function initTable() {
    $('#example').removeClass('table table-bordered');//these mess it up
    oTable = $('#example').dataTable({
        "sScrollY": "400px",
        "sScrollX": "100%",
        //"bScrollCollapse": true,
        "bPaginate": false,
        //"bSort": false,
        "bSearchable": false,
    });
    fc = new $.fn.dataTable.FixedColumns(oTable);
}


