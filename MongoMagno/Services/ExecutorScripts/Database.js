var Database = function() {

};

Database.prototype = {
    
    addCollection: function(name) {
        this[name] = new Collection(name);
    }

};
