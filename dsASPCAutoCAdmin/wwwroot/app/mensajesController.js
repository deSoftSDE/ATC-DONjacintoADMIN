appadmin.controller('Mensajes', function ($scope, Llamada, $timeout) {
    console.log("Holi");
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };

    LeerRegistros = function (obj, objmodificado) {
        $scope.lastConsulta = JSON.parse("" + JSON.stringify(obj));
        Llamada.post("LecturasGenericasPaginadas", obj)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (obj.accionPagina) {
                        case "N":
                            cambiarBotonesPaginacionFinales("disabled");
                            break;
                        case "P":
                            cambiarBotonesPaginacionIniciales("disabled");
                            break;
                    }

                } else {
                    $scope.vm = respuesta.data;
                    $scope.orders = respuesta.data.articulos;
                    if (NotNullNotUndefinedNotEmpty(objmodificado)) {
                        $scope.orders.splice(0, 0, objmodificado);
                    }
                    for (i = 0; i < $scope.orders.length; i++) {
                        $scope.orders[i].url = Llamada.getRuta($scope.orders[i].imagen);
                    }
                    console.log($scope.orders);
                    console.log("Arriba las orders");
                    $scope.datagrid.option("dataSource", $scope.orders);
                }
                //$scope.datagrid.repaint();
            });
    };


    $scope.dataGridOptions = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
        dataSource: [],
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        masterDetail: {
            enabled: false,
            template: "detail"
        },
        onRowInserted: function (info, a) {
            console.log(info);
            console.log(a);
        },
        onInitNewRow: function (info, b) {
            console.log("init");
            console.log(info);
            info.data.descripcion = 'Nuevo';
            console.log("HOLIII");
            info.component.saveEditData();
        },
        /*onSelectionChanged: function (e) {
            console.log(e);
            alert("abierto");
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        columns: [
            {
                dataField: "nombre",
                width: "30%",
                caption: "Usuario"
            }, {
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                caption: "Empresa",
                cellTemplate: "editTemplate"
            }, {
                width: "20%",
                dataField: "activo",
                caption: "Activo"
            }, {
                width: "20%",
                dataField: "ultimoAcceso",
                dataType: 'date',
                format: "MM/dd/yyyy HH:mm",
                caption: "Último acceso"
            }, {
                width: "20%",
                dataField: "ipAddress",
                caption: "Dirección IP"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alingment: "center",
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
            var obj = {
                tipo: "UsuariosWeb",
                cadena: ""
            };
            LeerRegistros(obj);
        },
        onRowUpdated: function (e) {
            alert("Hola");
            console.log(e);
        },
        onRowUpdating: function (e) {
            alert("Hola");
            console.log(e);
        }
    }

    $scope.openPopup = function () {
        $scope.popupVisible = true;
    }
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: "auto",
        showTitle: true,
        title: "Enviar a un Usuario",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: false
    };

    $scope.seleccionarUsuario = function (Us) {
        console.log(Us);
        $scope.popupVisible = false;
        document.getElementById("nombrecliente").value = Us.data.nombreCompleto;
        document.getElementById("idUsuarioWeb").value = Us.data.idUsuarioWeb;
    };
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    }
    $scope.todosUsuarios = function () {
        document.getElementById("nombrecliente").value = "Todos";
        document.getElementById("idUsuarioWeb").value = 0;
        document.getElementById("idcliente").value = 0;
    }
});