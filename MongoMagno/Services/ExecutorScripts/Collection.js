var Collection = function (name) {

    var self = this;
    self.captures = [];
    self.collectionName = name;
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

