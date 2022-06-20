function ButtonCalculateClick() {
    var precision = document.getElementById('precision').value;
    var number = document.getElementById('number').value;
    var answer = '';
    if (precision > 0 || precision.replace(/^00+/, '0') == '0') {   // Ветка, если точность выставлена
        answer = math.evaluate('sqrt(' + number + ')');             // Считаем через math.js корень из полученного выражения
        var partsOfAnswer = [];                                     // Массив вида ['a'{+'i', '+'|'-', 'bi'}]
        partsOfAnswer = answer.toString().split(' ');       // Разделяем результирующую строку вида "a" или "a + bi" на составляющие по пробелу
        // Вытаскиваем из а содержимое, взяв подстроку с 0 позиции до точки, если она есть в строке, иначе взять  всю строку
        var beforeDot = '';
        if (partsOfAnswer[0].indexOf('.') == -1)
            beforeDot = partsOfAnswer[0];
        else
            beforeDot = partsOfAnswer[0].substr(0, partsOfAnswer[0].indexOf('.'));

        var AfterDot = '';
        //Если точка есть и точность указана != 0
        if (partsOfAnswer[0].indexOf('.') != -1 && precision != 0) {
            //копировать содержимое после точки, после чего из этого содержимого копировать количество знаков = precision
            var AfterDot = ( partsOfAnswer[0].substr(partsOfAnswer[0].indexOf('.') + 1) ).substr(0, precision);
            if (number < 0)
                answer = beforeDot + '.' + AfterDot + 'i';
            else
                answer = beforeDot + '.' + AfterDot;
        }
        //Если точки нет или точность = 0
        else
            answer = beforeDot;
        //Если у нас не одно число, а сумма/разность
        if (partsOfAnswer.length > 1) {
            // Вытаскиваем из bi содержимое, взяв подстроку с 0 позиции до точки, если она есть в строке, иначе взять всю строку
            var beforeDot = '';
            if (partsOfAnswer[2].indexOf('.') == -1)
                beforeDot = partsOfAnswer[2];
            else
                beforeDot = partsOfAnswer[2].substr(0, partsOfAnswer[0].indexOf('.'));
            var AfterDot = '';
            //Если точка есть и точность указана != 0
            if (partsOfAnswer[2].indexOf('.') != -1 && precision != 0) {
                //копировать содержимое после точки, после чего из этого содержимого копировать количество знаков = precision
                var AfterDot = (partsOfAnswer[2].substr(partsOfAnswer[2].indexOf('.') + 1)).substr(0, precision);
                // после чего из этого содержимого копировать количество знаков = precision
                answer = answer + ' ' + partsOfAnswer[1] + ' ' + beforeDot + '.' + AfterDot + 'i';
            }
            //Если точки нет или точность = 0
            else
                answer = answer + ' ' + partsOfAnswer[1] + ' ' + beforeDot + 'i';
        }
    }
    //Ветка, если точности нет
    else {
        document.getElementById('precision').value = '';
        answer = math.evaluate('sqrt(' + number + ')');
    }
    //Если из-а большого значения точности после точки в знаки взялся i и он помимо этого был добавлен дополнительно, то исправить
    if (answer.toString().indexOf('ii') != -1)
        answer = answer.replace('ii', 'i');
    // Если у нас выражение, где сумма или разность, нужно обернуть его в скобки
    if (answer.toString().includes('+') || answer.toString().includes('-'))
        document.getElementById('answer1').value = '±(' + answer + ')';
    else
        document.getElementById('answer1').value = '±' + answer;
}