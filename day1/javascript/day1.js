
const data = require('./datainput.json');

//part 1
function GetTotalIncreases(inputArray) {
    
    let prevValue = 0;
    let totalIncreases = 0;

    for (let index = 0; index < inputArray.length; index++) {

        if(index !== 0 && prevValue < inputArray[index]) {
            totalIncreases++
        }

        prevValue = inputArray[index]
    }

    return totalIncreases
}

console.log("Part 1 answer: " + GetTotalIncreases(data))

//part 2

function GetSlidingIncreases(inputArray) {
    let prevSum = 0;
    let totalIncreases = 0;

    for (let i = 0; i < inputArray.length; i++) {
        
        let currentSum = GetSums(inputArray[i], inputArray[i + 1], inputArray[i + 2])

        if(i !== 0 && prevSum < currentSum) {
            totalIncreases++
        }
        
        prevSum = currentSum;
    }

    return totalIncreases;
}

function GetSums(val1, val2, val3) {
    return val1 + val2 + val3
}

console.log("Part 2 answer: " + GetSlidingIncreases(data))