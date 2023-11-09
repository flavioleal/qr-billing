db.createUser({
    user: "leal",
    pwd: "leal",
    roles: [{ role: "readWrite", db: "billing" }]
});
