/// <reference path="../jquery/jquery.d.ts" />
/// <reference path="../knockout/knockout.d.ts" />

module StarLightInternal {
    export class CodeReview {
        public userList = ko.observableArray<string>();
        public user = ko.observable<string>();
        public lotterUsers = ko.observableArray<LotterUserViewModel>();
        public randomNum = ko.observable<number>();
        public doc = ko.observable<string>("");
        public hrefBody = ko.observable<string>();
        public isSendEmail = ko.observable<boolean>(false);
        public time: number;
        public emailAddress = ko.observableArray<string>();
        constructor() {
            ko.applyBindings(this);
            var self = this;
            $.get("/Users.json", function (data) {
                $.each(data, function (index, item) {
                    var email = item["email"];
                    var name = email.split("@starlight-sms.com");
                    self.userList.push(name[0]);
                    self.emailAddress().push(email);
                })
            });
            this.lotterUsers.push(new LotterUserViewModel("星期一"));
            this.lotterUsers.push(new LotterUserViewModel("星期二"));
            this.lotterUsers.push(new LotterUserViewModel("星期三"));
            this.lotterUsers.push(new LotterUserViewModel("星期四"));
            this.lotterUsers.push(new LotterUserViewModel("星期五"));
        }

        public Lotter() {
            var self = this;
            var lottersLength = self.lotterUsers().length;
            for (var i = 0; i < lottersLength; i++) {
                var lotterUser = this.lotterUsers()[i];
                var codeReviewUser = lotterUser.codeReviewUser;
                var updateCodeUser = lotterUser.updateCodeUser;
                if (codeReviewUser() == null || codeReviewUser() == "") {
                    codeReviewUser(this.user());
                    self.doc(self.doc() + lotterUser.week + " code review : " + this.user());
                    return;
                }
                if (updateCodeUser() == null || updateCodeUser() == "") {
                    if (codeReviewUser() == this.user()) {
                        self.Rotate();
                        return;
                    }
                    updateCodeUser(this.user());

                    self.doc(self.doc() + "," + " update Code : " + this.user() + "</br>");
                    if (i == lottersLength - 1) {
                        clearInterval(self.time);
                        self.isSendEmail(true);
                        this.hrefBody("mailto:" + self.emailAddress() + "?Subject=code_review&Body=" + this.doc());
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
        public emailAddress = ko.observable<string>();
        public updateCodeUser = ko.observable<string>();
    }
}