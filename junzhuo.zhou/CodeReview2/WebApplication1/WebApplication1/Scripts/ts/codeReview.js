/// <reference path="../jquery/jquery.d.ts" />
/// <reference path="../knockout/knockout.d.ts" />
var StarLightInternal;
(function (StarLightInternal) {
    var CodeReview = (function () {
        function CodeReview() {
            this.userList = ko.observableArray();
            this.user = ko.observable();
            this.lotterUsers = ko.observableArray();
            this.randomNum = ko.observable();
            ko.applyBindings(this);
            this.userList.push("ZhiDa");
            this.userList.push("gray");
            this.userList.push("yunlong");
            this.userList.push("jianluo");
            this.userList.push("peach");
            this.userList.push("robot");
            this.userList.push("Zhun");
            this.userList.push("Zuo");
            this.userList.push("Hao");
            this.userList.push("jason");
            this.userList.push("Speed");
            this.userList.push("Ken");
            this.userList.push("junzhuo");
            this.lotterUsers.push(new LotterUserViewModel("星期一"));
            this.lotterUsers.push(new LotterUserViewModel("星期二"));
            this.lotterUsers.push(new LotterUserViewModel("星期三"));
            this.lotterUsers.push(new LotterUserViewModel("星期四"));
            this.lotterUsers.push(new LotterUserViewModel("星期五"));
        }
        CodeReview.prototype.Lotter = function () {
            var self = this;
            self.userList.remove(self.user());
            var lottersLength = self.lotterUsers().length;
            for (var i = 0; i < lottersLength; i++) {
                var lotterUser = this.lotterUsers()[i];
                var codeReviewUser = lotterUser.codeReviewUser;
                var updateCodeUser = lotterUser.updateCodeUser;
                if (codeReviewUser() == null || codeReviewUser() == "") {
                    codeReviewUser(this.user());
                    return;
                }
                if (updateCodeUser() == null || updateCodeUser() == "") {
                    updateCodeUser(this.user());
                    if (i == lottersLength - 1) {
                        clearInterval(self.time);
                        self.user("抽奖结束");
                    }
                    return;
                }
            }
        };
        ;
        CodeReview.prototype.EnterLotter = function () {
            var self = this;
            window.document.onkeyup = function (e) {
                if (e.keyCode == 13) {
                    self.Lotter();
                }
            };
        };
        CodeReview.prototype.Rotate = function () {
            var index = this.getRandom(0, this.userList().length - 1);
            var user = this.userList()[index];
            this.user(user);
        };
        CodeReview.prototype.getRandom = function (min, max) {
            var r = Math.random() * (max - min);
            var re = Math.round(r + min);
            re = Math.max(Math.min(re, max), min);
            return re;
        };
        CodeReview.prototype.RemainTime = function () {
            var self = this;
            self.time = window.setInterval(self.Rotate.bind(self), 10);
        };
        return CodeReview;
    }());
    StarLightInternal.CodeReview = CodeReview;
    var LotterUserViewModel = (function () {
        function LotterUserViewModel(_week) {
            this.codeReviewUser = ko.observable();
            this.updateCodeUser = ko.observable();
            this.week = _week;
        }
        return LotterUserViewModel;
    }());
    StarLightInternal.LotterUserViewModel = LotterUserViewModel;
})(StarLightInternal || (StarLightInternal = {}));
