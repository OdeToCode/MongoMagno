var Collection = function () {

    var self = this;
    self.captures = [];
    var commands = ["find", "findOne", "limit", "orderBy"];

    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = function (name) {

            return function () {
                self.captures.push({ name: name, args: Array.prototype.slice.call(arguments) });
                return self;
            };

        }(commands[i]);
    }      
};

