$(function () {
    $("#gridContainer").dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            load: function () {
                return $.ajax({
                    url: "/Users/UserData",
                    type: "GET",
                    dataType: "json"
                });
            },
            insert: function (values) {
                // Insert new row logic (typically send to the server)
                return $.ajax({
                    url: '/Auth/UserRegistration', // Your create API endpoint
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(values)
                });
            },
            update: function (key, values) {
                values.id = key.id;
                return $.ajax({
                    url: '/Users/UserModification',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(values),
                    success: function (response) {
                        console.log("Update successful:", response);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.error("Update failed:", textStatus, errorThrown);
                    }
                });
            },
            remove: function (key) {
                return $.ajax({
                    url: '/Users/UserDelation?id=' + key.id,
                    type: 'GET'
                });
            }
        }),
        remoteOperations: false,
        columns: [
            { dataField: "id", caption: "ID", visible: false, allowEditing: false },
            { dataField: "userName", caption: "Name", validationRules: [{ type: "required" }] },
            { dataField: "email", caption: "Email", validationRules: [{ type: "email" }, { type: "required" }] },
            { dataField: "isVerified", caption: "Verified", dataType: "boolean" },
            { dataField: "password", caption: "Password", visible: false } // Hide the password column in the grid
        ],
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            form: {
                customizeItem: function (item) {
                    if (item.dataField === "password") {
                        // Show password only in insert mode
                        item.visible = true;
                    }
                }
            }
        },
        onEditorPreparing: function (e) {
            if (e.dataField === "password") {
                if (!e.row.isNewRow) {
                    e.cancel = true; // Hide the password field in edit mode
                } else {
                    e.editorOptions.mode = "password"; // Show password in insert mode
                    e.editorOptions.placeholder = "Enter password";
                }
            }
        },

        onRowInserting: function (e) {
            console.log("Inserting row:", e.data);
        },
        onRowUpdating: function (e) {
            console.log("Updating row:", e.key, e.newData);
        },
        onRowRemoving: function (e) {
            console.log("Deleting row with ID:", e.key);
        },
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        }
    });
});

