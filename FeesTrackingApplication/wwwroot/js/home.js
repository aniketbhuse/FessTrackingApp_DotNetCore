$(document).ready(function () {

    /* ===============================
       GLOBAL DATA ARRAY
    ================================ */
    let batches = [];

    /* ===============================
       PAGINATION VARIABLES
    ================================ */
    let pageSize = 10;
    let currentPage = 1;

    /* ===============================
       FETCH DATA FROM ASP.NET CORE API
    ================================ */
    function loadBatchesFromApi() {
        debugger;
        $.ajax({
            url: "/api/batches",   // ASP.NET Core API
            type: "GET",
            success: function (data) {
                debugger;
                batches = data.map(b => ({
                    id: b.batchId,
                    name: b.batchName,
                    desc: b.description,
                    start: new Date(b.startDate).toLocaleDateString(),
                    end: new Date(b.endDate).toLocaleDateString(),
                    active: b.isActive
                }));
                debugger;
                console.log("Total batches fetched:", batches.length);
                console.log("Fetched data:", data);
                console.log("Mapped batches:", batches);
                console.log("Displaying page", currentPage, "records:", batches.slice((currentPage - 1) * pageSize, currentPage * pageSize));

                currentPage = 1;
                paginate();
            },
            error: function (err) {
                debugger; 
                console.error("Error loading batches", err);
                alert("Failed to load batch data");
            }
        });
    }

    /* ===============================
       RENDER TABLE
    ================================ */
    function renderTable(data) {
        let html = "";

        $.each(data, function (i, b) {
            html += `
                <tr>
                    <td>${b.id}</td>
                    <td>${b.name}</td>
                    <td>${b.desc}</td>
                    <td>${b.start}</td>
                    <td>${b.end}</td>
                    <td class="${b.active ? 'active' : 'inactive'}">
                        ${b.active ? '✔ Active' : '✖ Inactive'}
                    </td>
                    <td>
                        <button class="icon-btn edit" data-id="${b.id}">✏</button>
                        <button class="icon-btn delete" data-id="${b.id}">🗑</button>
                    </td>
                </tr>`;
        });

        $("#tableBody").html(html);
    }

    /* ===============================
       RENDER CARDS
    ================================ */
    function renderCards(data) {
        let html = "";

        $.each(data, function (i, b) {
            html += `
                <div class="batch-card">
                    <h3>${b.name}</h3>
                    <p>${b.desc}</p>
                    <p><strong>Start:</strong> ${b.start}</p>
                    <p><strong>End:</strong> ${b.end}</p>
                    <span class="badge ${b.active ? 'active' : 'inactive'}">
                        ${b.active ? 'Active' : 'Inactive'}
                    </span>
                </div>`;
        });

        $("#cardContainer").html(html);
    }

    /* ===============================
       PAGINATE DATA
    ================================ */
    function paginate() {
        let start = (currentPage - 1) * pageSize;
        let end = start + pageSize;

        let pageData = batches.slice(start, end);

        renderTable(pageData);
        renderCards(pageData);
        renderPagination();
    }

    /* ===============================
       PAGINATION BUTTONS
    ================================ */
    function renderPagination() {
        let pageCount = Math.ceil(batches.length / pageSize);
        let html = "";

        for (let i = 1; i <= pageCount; i++) {
            html += `
                <button class="${i === currentPage ? 'active' : ''}" data-page="${i}">
                    ${i}
                </button>`;
        }

        $("#pagination").html(html);
    }

    /* ===============================
       PAGE CLICK EVENT
    ================================ */
    $("#pagination").on("click", "button", function () {
        currentPage = parseInt($(this).data("page"));
        paginate();
    });

    /* ===============================
       VIEW TOGGLE
    ================================ */
    $("#tableViewBtn").click(function () {
        $(".toggle-btn").removeClass("active");
        $(this).addClass("active");
        $("#tableView").show();
        $("#cardView").hide();
    });

    $("#cardViewBtn").click(function () {
        $(".toggle-btn").removeClass("active");
        $(this).addClass("active");
        $("#tableView").hide();
        $("#cardView").show();
    });

    /* ===============================
       INITIAL LOAD
    ================================ */
    loadBatchesFromApi();

});
