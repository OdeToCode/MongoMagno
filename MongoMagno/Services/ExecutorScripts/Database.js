var Database = function(name) {
    this.name = name;
};

Database.prototype = {
    
    addCollection: function(name) {
        this[name] = new Collection(name);
    }

};
