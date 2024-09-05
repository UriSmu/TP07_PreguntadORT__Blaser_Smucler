var difficulties = ["Fácil", "Media", "Alta"];
var startAngle = 0;
var arc = Math.PI / (difficulties.length / 2);
var spinTimeout = null;

var spinArcStart = 10;
var spinTime = 0;
var spinTimeTotal = 0;

var ctx;

document.getElementById("spinButtonDificultad").addEventListener("click", spin);

function byte2Hex(n) {
  var nybHexString = "0123456789ABCDEF";
  return String(nybHexString.substr((n >> 4) & 0x0F,1)) + nybHexString.substr(n & 0x0F,1);
}

function RGB2Color(r,g,b) {
  return '#' + byte2Hex(r) + byte2Hex(g) + byte2Hex(b);
}

function getColor(item, maxitem) {
  var phase = 0;
  var center = 128;
  var width = 127;
  var frequency = Math.PI*2/maxitem;
  
  var red   = Math.sin(frequency*item+2+phase) * width + center;
  var green = Math.sin(frequency*item+0+phase) * width + center;
  var blue  = Math.sin(frequency*item+4+phase) * width + center;
  
  return RGB2Color(red,green,blue);
}

function drawRouletteWheel() {
    var canvas = document.getElementById("canvasDificultad");
    // Asegúrate de que el canvas tenga un tamaño suficiente para mostrar la ruleta completa.
    canvas.width = 500;   // Ancho del canvas
    canvas.height = 500;  // Alto del canvas
  
    if (canvas.getContext) {
      var outsideRadius = 220;  // Radio externo de la ruleta
      var textRadius = 180;     // Radio para colocar el texto
      var insideRadius = 100;   // Radio interno de la ruleta
  
      ctx = canvas.getContext("2d");
      ctx.clearRect(0, 0, canvas.width, canvas.height);
  
      ctx.strokeStyle = "black";
      ctx.lineWidth = 2;
  
      ctx.font = 'bold 14px Helvetica, Arial';
  
      for(var i = 0; i < difficulties.length; i++) {
        var angle = startAngle + i * arc;
        ctx.fillStyle = getColor(i, difficulties.length);
  
        ctx.beginPath();
        ctx.arc(250, 250, outsideRadius, angle, angle + arc, false);
        ctx.arc(250, 250, insideRadius, angle + arc, angle, true);
        ctx.stroke();
        ctx.fill();
  
        ctx.save();
        ctx.fillStyle = "black";
        ctx.translate(250 + Math.cos(angle + arc / 2) * textRadius, 
                      250 + Math.sin(angle + arc / 2) * textRadius);
        ctx.rotate(angle + arc / 2 + Math.PI / 2);
        var text = difficulties[i];
        ctx.fillText(text, -ctx.measureText(text).width / 2, 0);
        ctx.restore();
      }
  
      // Flecha (opcional)
      ctx.fillStyle = "black";
      ctx.beginPath();
      ctx.moveTo(250 - 4, 250 - (outsideRadius + 5));
      ctx.lineTo(250 + 4, 250 - (outsideRadius + 5));
      ctx.lineTo(250 + 4, 250 - (outsideRadius - 5));
      ctx.lineTo(250 + 9, 250 - (outsideRadius - 5));
      ctx.lineTo(250 + 0, 250 - (outsideRadius - 13));
      ctx.lineTo(250 - 9, 250 - (outsideRadius - 5));
      ctx.lineTo(250 - 4, 250 - (outsideRadius - 5));
      ctx.lineTo(250 - 4, 250 - (outsideRadius + 5));
      ctx.fill();
    }
  }
  

function spin() {
  spinAngleStart = Math.random() * 10 + 10;
  spinTime = 0;
  spinTimeTotal = Math.random() * 3 + 4 * 1000;
  rotateWheel();
}

function rotateWheel() {
  spinTime += 30;
  if(spinTime >= spinTimeTotal) {
    stopRotateWheel();
    return;
  }
  var spinAngle = spinAngleStart - easeOut(spinTime, 0, spinAngleStart, spinTimeTotal);
  startAngle += (spinAngle * Math.PI / 180);
  drawRouletteWheel();
  spinTimeout = setTimeout(rotateWheel, 30);
}

function stopRotateWheel() {
  clearTimeout(spinTimeout);
  var degrees = startAngle * 180 / Math.PI + 90;
  var arcd = arc * 180 / Math.PI;
  var index = Math.floor((360 - degrees % 360) / arcd);
  
  var selectedDifficulty = difficulties[index];
  document.getElementById("selectedDificultad").textContent = selectedDifficulty;
  
  // Poner el valor seleccionado en el input oculto
  document.getElementById("idDificultadInput").value = selectedDifficulty;

  // Esperar 3 segundos y enviar el formulario
  setTimeout(function() {
    document.getElementById("difficultyForm").submit();
  }, 3000);
}

function easeOut(t, b, c, d) {
  var ts = (t/=d)*t;
  var tc = ts*t;
  return b+c*(tc + -3*ts + 3*t);
}

// Dibuja la ruleta al cargar la página
drawRouletteWheel();