var self = this;
self.environment = function () {
    
    var createDatabase = function () {
        self.db = new Database();
    };

    var createCollections = function(names) {
        for (var i = 0; i < names.Length; i++) {           
            self.db.addCollection(names[i]);
        }        
    };
  
    return {
        createDatabase: createDatabase,
        createCollections: createCollections      
    };

}();