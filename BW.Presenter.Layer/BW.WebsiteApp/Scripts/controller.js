var app = angular.module('createUser', ['matchPassword']);

angular.module('matchPassword', []).directive('pwCheck', [function () {
      return {
          require: 'ngModel',
          link: function (scope, elem, attrs, ctrl) {
              var firstPassword = '#' + attrs.pwCheck;
              elem.add(firstPassword).on('keyup', function () {
                  scope.$apply(function () {
                      var v = elem.val() === $(firstPassword).val();
                      ctrl.$setValidity('pwmatch', v);
                  });
              });
          }
      }
}]);

app.controller('validEmailCtrl', function ($scope) {
    $scope.checkValidEmail = true;
    $scope.regexp = '';// /[a-zA-Z0-9]+[\\.a-zA-Z0-9]*@[a-zA-Z0-9+\\.]/
    
    $scope.$watch("checkValidEmail", function (newValue, oldValue) {
            if (newValue) {

                $scope.regexp = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            }
            else {
                $scope.regexp = "";
            }
        }
    );
});

//app.controller('getAllCtrl', function ($scope, $http) {
//    $http.get("http://localhost:8793/api/User/getalluser").success(function (response) {
//        $scope.names = response;
//    });
//});