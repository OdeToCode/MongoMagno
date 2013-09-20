var Collection = function (name) {

    var self = this;
    self.captures = [];
    self.collectionName = name;
    var commands = ["find", "findOne", "limit", "orderBy"];

    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = function (commandName) {

            return function () {
                self.captures.push({ name: commandName, args: Array.prototype.slice.apply(arguments) });
                return self;
            };

        }(commands[i]);
    }      
};

