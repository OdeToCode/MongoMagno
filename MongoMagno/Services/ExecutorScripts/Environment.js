var self = this;
self.environment = function () {

    var createDatabase = function(name) {
        self.db = new Database(name);
    };

    var createCollections = function(names) {
        for (var i in names) {
            self.db.addCollection(names[i]);
        }        
    };

    return {
        createDatabase: createDatabase,
        createCollections: createCollections
    };

}();