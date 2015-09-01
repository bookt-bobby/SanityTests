<%@ Page Title="Test Summary" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Tests.aspx.vb" Inherits="Tests_Tests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        'use strict';
        var app = angular.module('app', [
          //'ngRoute'
        ]);

        app.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider.
              when('/tests/tests/addTest', {
                  templateUrl: 'templates/add-test.html',
                  controller: 'TestsController'
              }).
              when('/tests/tests/showTests', {
                  templateUrl: 'templates/show-tests.html',
                  controller: 'TestsController'
              }).
              otherwise({
                  redirectTo: '/addOrder'
              });
        }]);
       

        function TestsController($scope,$http) {
            $http.get('/ws/?method=get').success(function(data){
                $scope.tests = data.result;
            });
            $scope.delete = function () {
                var id=this.t.Id
                $http.post('/ws/?method=delete&id=' + id);
            };
            $scope.save = function () {
                console.log(this);
                $http.post('/ws/?method=save', JSON.stringify(this.t), function () {

                });
            };
            $scope.validate = function () {
                var tests = $scope.tests;
                $(tests).each(function (i) {
                    console.log(this);
                    var url = this.Url;
                    var property = this.Property;
                    var value = this.Value;
                    if(url ==null || url=='') return ;//equivalent of continue in each()
                    var jqxhr = $.get(url, function (data) {
                        
                        tests[i].Response = JSON.stringify(data);
                        try {
                            if (deepFind(data, property) == value) {
                                tests[i].TestSuccess = 'Passed'; 
                            }
                            else {
                                tests[i].TestSuccess = 'Failed';
                            }
                        } catch (e) {
                            console.log(e.message);
                        }
                       
                    })
                      .done(function () {})
                      .fail(function () {
                          $scope.tests[i].TestSuccess = 'Failed';
                      })
                      .always(function () {
                          $scope.$apply();
                      });
                });
            }
            
        }

        function deepFind(obj, path) {
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

      
        
    </script>
<div >
  <h4> Tests</h4>
  <div ng-controller="TestsController">
      <div >  <input type="button" ng-click="validate()"    value="Validate" />
        <table class="table table-striped table-bordered table-condensed">
           <tr  class="test-row" ng-repeat="t in tests">
            <td><input type="button" value="-" title="delete" ng-click="delete()" /> </td>
            <td><input    ng-bind="t.Test"  ng-model="t.Test" /></td>
            <td><input    ng-blur="save()"  type="text" ng-bind="t.Url"  ng-model="t.Url" /></td>
            <td><input    ng-blur="save()"  type="text" ng-bind="t.Property" ng-model="t.Property"  /></td>
            <td><input    ng-blur="save()"  type="text" ng-bind="t.Value" ng-model="t.Value"  /></td>
            <td ><input   ng-blur="save()"  type="text" ng-model="t.TestSuccess" /></td>
            <td><textarea ng-blur="save()"  rows ="5"    ng-model="t.Response" /></td>
            <td><input    ng-blur="save()"  type="text" ng-bind="t.PostData" ng-model="t.PostData" /></td> 
            <td><input    ng-blur="save()"  type="text" ng-bind="t.Method" ng-model="t.Method" /></td>
            <td><input  ng-blur="save()"  type="text" ng-bind="t.ContentType" ng-model="t.ContentType" /></td>
               
           </tr>
        </table>
      </div>
    
   
  </div>


</div>

    

    
    {{searchText}}
</asp:Content>

