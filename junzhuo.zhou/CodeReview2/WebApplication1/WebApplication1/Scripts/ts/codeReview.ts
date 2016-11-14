/// <reference path="../jquery/jquery.d.ts" />
/// <reference path="../knockout/knockout.d.ts" />
module StarLightInternal {
    export class CodeReview {
        public userList = ko.observableArray<string>();
        public user = ko.observable<string>();
        public lotterUsers = ko.observableArray<LotterUserViewModel>();
        public randomNum = ko.observable<number>();
        public time: number;
        constructor() {
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
        public Lotter() {
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
        public EnterLotter() {
            var self = this;
            window.document.onkeyup = function (e) {
                if (e.keyCode == 13) {
                    self.Lotter();
                }
            }
        }
        public Rotate() {
            var index = this.getRandom(0, this.userList().length - 1);
            var user = this.userList()[index];
            this.user(user);
        }
        private getRandom(min, max) {
            var r = Math.random() * (max - min);
            var re = Math.round(r + min);
            re = Math.max(Math.min(re, max), min)
            return re;
        }
        public RemainTime() {
            var self = this;
            self.time = window.setInterval(self.Rotate.bind(self), 10);
        }
    }
    export class LotterUserViewModel {
        constructor(_week: string) {
            this.week = _week;
        }
        public week: string;
        public codeReviewUser = ko.observable<string>();
        public updateCodeUser = ko.observable<string>();
    }
}