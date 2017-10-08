var list = [];
function updateItemStatus(itemID, value) {
    $.post("/Item/AtualizaStatus",
        {
            id: itemID,
            status: value
        });
}

function updateItemLocalidade(itemID) {
    var localidade_id = document.getElementById('localidade_id').value;
    $.post("/Item/AtualizaLocalidade",
        {
            id: itemID,
            localidade: localidade_id
        });
    $('#modalMoverItem').modal('hide');
    exibeMensagem();
}

function exibeMensagem() {
    var div_msg = document.getElementById('msg');
    div_msg.innerHTML = '<div class="alert alert-warning text-center">'
        + '<button type="button" class="close" data-dismiss="alert" aria-label="Close">'
        + '<span aria-hidden="true">&times;</span></button>'
        + 'Item adicionado. <a href="#" onclick="redireciona();" class="alert-link">Clique aqui</a> para recarregar a listagem</div >';
}

function redireciona() {
    var localidade_id = document.getElementById('localidade_id').value;
    var url = '/Inventario/UpdateViaJS';
    var form = $('<form action="' + url + '" method="post">' +
        '<input type="text" name="localidade_id" value="' + localidade_id + '" /></form>');
    $('body').append(form);
    form.submit();
}

function updateItemObs(itemID) {
    var elem_name = "input_obs_" + itemID;
    var value = document.getElementById(elem_name).value;
    $.post("/Item/AtualizaObs",
        {
            id: itemID,
            observacao: value
        });
}

function showButton(id) {
    var btn = document.getElementById("btn_obs_" + id);
    btn.style.visibility = "visible";
}

function hideButton(id) {
    var btn = document.getElementById("btn_obs_" + id);
    btn.style.visibility = "hidden";
}

function filterItens(text) {
    var filtered_list = [];

    if (list.length == 0) {
        loadListFromStorage();
    }
    if (text != "") {
        var localidade_id = document.getElementById('localidade_id').value;
        for (var i = 0; i < list.length; i++) {
            if (list[i].patrimonio.indexOf(text) != -1 && list[i].id_local != localidade_id) {
                filtered_list.unshift(list[i]);
            }
        }
        setListToHTML(filtered_list);
    } else {
        setListToHTML("");
    }
}

function setListToHTML(list) {
    var tbody = document.getElementById("tbody_itens");
    var table_content = "";

    for (var key in list) {
        table_content += '<tr><td>' + list[key].patrimonio + '</td>'
            + '<td>' + list[key].categoria + '</td>'
            + '<td>' + list[key].localidade + '</td>'
            + '<td> <button class="btn btn-success" onclick="updateItemLocalidade(' + list[key].id + ')";>Mover</button>';
    }
    tbody.innerHTML = table_content;
}

function saveListOnStorage() {
    var jsonStr = JSON.stringify(list);
    localStorage.setItem("list", jsonStr);
}

function loadListFromStorage() {
    var itens = localStorage.getItem("list");
    if (itens) {
        list = JSON.parse(itens);
    }
}

