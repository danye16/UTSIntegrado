function agregarDato() {
    let hora = document.getElementById("hora").value;
    let diaIndex = document.getElementById("dia").value;
    let dato = document.getElementById("dato").value;

    let table = document.getElementById("tabla");
    let rows = table.getElementsByTagName("tr");

    for (let i = 1; i < rows.length; i++) { 
      let cells = rows[i].getElementsByTagName("td");
      if (cells[0].innerHTML === hora) {
        for (let j = 1; j < cells.length; j++) {
          if (j === parseInt(diaIndex)) {
            cells[j].innerHTML = dato;
            break;
          }
        }
        break;
      }
    }

    document.getElementById("hora").value = "";
    document.getElementById("dia").value = "";
    document.getElementById("dato").value = "";
  }