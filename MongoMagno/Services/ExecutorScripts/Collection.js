var CommandCapture1 = function() {
    
    var commands = ["find", "findOne", "limit", "orderBy"];
    
    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = new Function("return { name: '" + commands[i] + "', args:arguments };");
    }
};

var CommandCapture = function () {

    var commands = ["find", "findOne", "limit", "orderBy"];

    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = function() {
            return {
                name: commands[i], // here's the problem
                args: arguments
            };
        };
    }
};

var CommandCapture3 = function () {

    var commands = ["find", "findOne", "limit", "orderBy"];

    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = function(name) {

            return function() {
                return {
                    name: name,
                    args: arguments
                };
            };
            
        }(commands[i]);
    }
};

var CommandCapture4 = function () {

    var self = this;
    self.$$captures = [];
    var commands = ["find", "findOne", "limit", "orderBy"];

    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = function (name) {

            return function () {
                self.$$captures.push({ name: name, args: arguments });
                return self;
            };

        }(commands[i]);
    }      
};

var c = new CommandCapture1();
var result = c.find({ x: 1, y: 3, name: "foo" }, { id: 0 });
console.log(result);

var c = new CommandCapture1();
var result = c.find({ x: 1, y: 3, name: "foo" }, { id: 0 });
console.log(result);

var c = new CommandCapture2();
var result = c.find({ x: 1, y: 3, name: "foo" }, { id: 0 });
console.log(result);

var c = new CommandCapture3();
var result = c.find({ x: 1, y: 3, name: "foo" }, { id: 0 });
console.log(result);

var c = new CommandCapture4();
var result = c.find({ x: 1, y: 3, name: "foo" }, { id: 0 }).limit(1);
console.log(result.$$captures);
