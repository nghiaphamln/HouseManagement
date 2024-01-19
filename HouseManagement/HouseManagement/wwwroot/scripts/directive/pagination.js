app.directive('paginationDirective', function () {
    return {
        restrict: 'E',
        scope: {
            pager: '=',
            onPageChange: '&'
        },
        template: `
            <div class="p-3">
                <i>Đang hiển thị <b>{{pager['displayFrom']}}</b> - <b>{{pager['displayTo']}}</b> của <b>{{pager['totalRecord']}}</b></i>
                <nav aria-label="Page navigation">
                    <ul class="pagination">
                        <li class="page-item prev" ng-class="{ 'disabled': pager['page'] === 1 }">
                            <a class="page-link" ng-click="goToPage(pager['page'] - 1)">
                                <i class="tf-icon bx bx-chevrons-left"></i>
                            </a>
                        </li>
                        <li ng-repeat="page in pager['pageRange']" class="page-item" ng-class="{ 'active': page === pager['page'] }">
                            <a class="page-link" ng-click="goToPage(page);">{{page}}</a>
                        </li>
                        <li class="page-item next" ng-class="{ 'disabled': pager['page'] === pager['totalPage'] }">
                            <a class="page-link" ng-click="goToPage(pager['page'] + 1)">
                                <i class="tf-icon bx bx-chevrons-right"></i>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
        `,
        link: function (scope) {
            scope.goToPage = function (page) {
                if (scope['onPageChange']) {
                    scope['onPageChange']({page});
                }
            }
        }
    };
});