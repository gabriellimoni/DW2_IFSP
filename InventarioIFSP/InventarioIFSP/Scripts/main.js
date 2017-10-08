$('.popover-dismiss').popover({
  trigger: 'focus'
})

function popInfo() {
    $('#info_admin').popover()
};

function limpaCampoBusca() {
    document.getElementById('campo_busca').value = "";
    filterItens("");
}

function excluirInventario() {
    var id = document.getElementById("input_inventario_id").value;
    var url = '/Inventario/Delete';
    var form = $('<form action="' + url + '" method="post">' +
        '<input type="text" name="inventario_id" value="' + id + '" /></form>');
    $('body').append(form);
    form.submit();
}
function defineIDExclusao(id) {
    document.getElementById('input_inventario_id').value = id;
}