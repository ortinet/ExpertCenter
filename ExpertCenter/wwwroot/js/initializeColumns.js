const userColumnsContainer = document.querySelector(".userColumns");

const addColumnButton = document.getElementById("addBtn");
addColumnButton.addEventListener("click", onAddColumnButtonClick);

let columnTypeOptions;

function onAddColumnButtonClick(e) {
    addColumn();
}

function initializeColumns(preparedColumns, columnTypes) {

    columnTypeOptions = columnTypes;

    preparedColumns.forEach((columnData) => {
        addColumn(columnData);
    })
}

function addColumn(columnData) {

    userColumnsContainer.appendChild(getNewRow(columnData));
}

function removeColumn() {

    this.parentElement.remove();
}

function getNewRow(columnData) {

    const columnNameInput = document.createElement("input");
    columnNameInput.className = "form-control";
    columnNameInput.placeholder = "Имя колонки";
    columnNameInput.name = "columnHeader";

    if (columnData != null) {
        columnNameInput.value = columnData.Header;
        columnTypeDropdown = getDropDown(columnData.Type.Code);
    }
    else {
        columnTypeDropdown = getDropDown();
    }

    const removeBtn = document.createElement("button");
    removeBtn.className = "btn btn-danger";
    removeBtn.type = "button";
    removeBtn.innerHTML = "&times";
    removeBtn.addEventListener("click", removeColumn);

    const row = document.createElement("div");
    row.style = "display: flex; margin-top: 5px;"
    row.className = "userColumnInputRow";

    row.appendChild(columnNameInput);
    row.appendChild(columnTypeDropdown);
    row.appendChild(removeBtn);

    return row;
}

function getDropDown(selectedType) {

    let selectHtml = document.createElement("select");
    selectHtml.className = "form-select";
    selectHtml.style = "width:auto; margin: 0px 5px"
    selectHtml.setAttribute("aria-label", "Default select example");
    selectHtml.name = "columnTypeCode"

    for (let i = 0; i < columnTypeOptions.length; i++) {

        let item = document.createElement("option");      

        item.value = columnTypeOptions[i].Code;
        item.innerText = columnTypeOptions[i].Title;

        if (selectedType == null) {
            if (i === 1) {
                item.selected = true;
            }
        }
        else if (item.value === selectedType)
            item.selected = true;

        selectHtml.appendChild(item);
    }

    return selectHtml;
}