let db;

window.LocalDb = {
    Initialize: function () {
        db = new Dexie('cookbook_database');
        db.version(1).stores({
            recipes: 'id',
            ingredients: 'id',
        });
    },
    GetAll: async function (tableName) {
        return await db.table(tableName).toArray();
    },
    GetById: async function (id) {
        let recipe = await db.table(tableName).get(id);
        return recipe;
    },
    Insert: function (tableName, entity) {
        db.table(tableName).put(entity);
    },
    Remove: async function (tableName, id) {
        await db.table(tableName).bulkDelete([id]);
    }
};
