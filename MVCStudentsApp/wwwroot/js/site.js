//var currentPage = 1;
//const studentsPerPage = 10;

//$(document).ready(function () {
//    // Substitua "students.json" pelo caminho correto ou pela lógica que você está utilizando para carregar os estudantes.
//    $.getJSON("students.json", function (students) {
//        window.students = students;
//        displayTable(students, currentPage);
//    });
//});

//function displayTable(students, currentPage) {
//    var index = (currentPage - 1) * studentsPerPage;
//    var endIndex = index + studentsPerPage;
//    let showStudents = $("tbody");
//    let out = "";

//    showStudents.empty(); // Limpa o conteúdo da tabela

//    for (i = index; i < endIndex && i < students.length; i++) {
//        var studentsObject = students[i];
//        out += '<tr>';
//        out += `<td>${studentsObject.Id}</td>`;
//        out += `<td>${studentsObject.FirstName}</td>`;
//        out += `<td>${studentsObject.LastName}</td>`;
//        out += `<td>${studentsObject.DateOfBirth}</td>`;
//        out += `<td>${studentsObject.FkRole == 1 ? "Student" : "Teacher"}</td>`;
//        out += '<td>';
//        out += `<a asp-action="Details" asp-route-id="${studentsObject.Id}" class="action-link">`;
//        out += `<img src="~/images/Details.png" alt="Details" title="Details" /></a> |`;
//        out += `<a asp-action="Edit" asp-route-id="${studentsObject.Id}" class="action-link">`;
//        out += `<img src="~/images/Edit.png" alt="Edit" title="Edit" /></a> |`;
//        out += `<a asp-action="Delete" asp-route-id="${studentsObject.Id}" class="action-link">`;
//        out += `<img src="~/images/Delete.png" alt="Delete" title="Delete" /></a>`;
//        out += '</td>';
//        out += '</tr>';
//    }

//    showStudents.append(out);
//    displayButtons(students);
//}

//function displayButtons(students) {
//    var total = students.length;
//    let showButtons = $("#buttons-container");
//    let out = "";

//    out += '<button class="buttons" onclick="previous()">Previous</button>';

//    for (i = 1; i <= Math.ceil(total / studentsPerPage); i++) {
//        out += `<button class="buttons" onclick="page(${i})">${i}</button>`;
//    }

//    out += '<button class="buttons" onclick="next()">Next</button>';

//    showButtons.html(out);
//}

//function page(content = 1) {
//    content = parseInt(content);
//    displayTable(window.students, content);
//    window.lastPage = content;
//}

//function previous() {
//    if (window.lastPage > 1) {
//        window.lastPage -= 1;
//    }
//    displayTable(window.students, window.lastPage);
//}

//function next() {
//    maxLimit = Math.ceil(window.students.length / studentsPerPage);

//    if (window.lastPage >= maxLimit) {
//        alert("Página não existe");
//    } else {
//        window.lastPage += 1;
//        displayTable(window.students, window.lastPage);
//    }
//}

//function search() {
//    var searchValue = $("#search").val().toLowerCase();
//    var results = [];

//    for (var i = 0; i < window.students.length; i++) {
//        if (
//            window.students[i].FirstName.toLowerCase().includes(searchValue) ||
//            window.students[i].LastName.toLowerCase().includes(searchValue)
//        ) {
//            results.push(window.students[i]);
//        }
//    }

//    if (results.length == 0) {
//        alert("ALERTA");
//        window.lastPage = 1;
//        displayTable(window.students, 1);
//    } else {
//        window.lastPage = 1;
//        displayTable(results, 1);
//    }
//}