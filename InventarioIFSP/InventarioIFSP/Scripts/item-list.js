var list = [];

function addItem(id, patrimonio, categoria, localidade, status, status_id) {
    list.unshift({ "id": id, "patrimonio": patrimonio, "categoria": categoria, "localidade": localidade, "status": status, "status_id": status_id });
}

function setListToHTML(list) {
    var tbody = document.getElementById("tbody-list-itens");
    var table_content = "";

    for (var key in list) {
        table_content += '<tr><td>' + list[key].patrimonio + '</td>'
            + '<td>' + list[key].categoria + '</td>'
            + '<td>' + list[key].localidade + '</td>'
            + '<td>' + list[key].status + '</td>'
            + '<td> <a class="btn btn-success" href="/Item/Details/' + list[key].id + '">Detalhes</a> | '
            + '<a class="btn btn-primary" href="/Item/Edit/' + list[key].id + '">Editar </a> |'
            + '<a class="btn btn-danger" href="/Item/Delete/' + list[key].id + '">Excluir</a>';
    }
    tbody.innerHTML = table_content;
}

function filterItensList() {
    console.log("atualiza");
    var text = document.getElementById('campo_busca').value;
    var status = document.getElementById('select_status').value;
    if(list.length == 0)
        list = loadListFromStorage();

    var filtered_list_sub = [];
    var filtered_list = [];


    if (text != "") {
        for (var i = 0; i < list.length; i++) {
            if (list[i].patrimonio.indexOf(text) != -1) {
                filtered_list_sub.unshift(list[i]);
            }
        }
    } else {
        filtered_list_sub = list;
    }

    if (status >= 0) {
        for (var i = 0; i < filtered_list_sub.length; i++) {
            if (filtered_list_sub[i].status_id == status) {
                filtered_list.unshift(filtered_list_sub[i]);
            }
        }
        setListToHTML(filtered_list);
    } else {
        setListToHTML(filtered_list_sub);
    }
}

function saveListOnStorage() {
    var jsonStr = JSON.stringify(list);
    localStorage.setItem("list", jsonStr);
}

function loadListFromStorage() {
    var itens = localStorage.getItem("list");
    if (itens) {
        list =  JSON.parse(itens);
    }
}