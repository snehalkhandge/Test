(function () {
    'use strict';

    var core = angular.module('app.core');

    // Configure Toastr
    toastr.options.timeOut = 6000;
    toastr.options.positionClass = 'toast-bottom-right';

    //var events = { };

    var config = {
        appErrorPrefix: '[HP Reality Error] ', //Configure the exceptionHandler decorator
        appTitle: 'HP Reality',
        apiUrl: 'http://localhost:10138',
        //        events: events,
        version: '1.0.0'
    };

    core.value('config', config);

    core.constant('toastr', toastr);

    core.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        $logProvider.debugEnabled(true);
        /*if ($logProvider.debugEnabled) {
           $logProvider.debugEnabled(true);
        }*/
    }]);

    // Configure the common route provider
    core.config(['$routeProvider', 'routehelperConfigProvider',
        function ($routeProvider, cfg) {
            cfg.config.$routeProvider = $routeProvider;
            cfg.config.docTitle = 'HP Reality: ';
        }]);

    // Configure the common exception handler
    core.config(['exceptionConfigProvider', function (cfg) {
        cfg.config.appErrorPrefix = config.appErrorPrefix;
    }]);
})();