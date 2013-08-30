var Collection = function(name) {
    this.name = name;
    this.isCollection = true;
    
    var commands = ["find"];
    
    for (var i = 0; i < commands.length; i++) {
        this[commands[i]] = new Function("return { name: '" + commands[i] + "', args:arguments };");
    }
};
