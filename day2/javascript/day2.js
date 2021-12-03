const data = require('../data.json');

let forward = 0;
let down = 0;
let depth = 0;
let aim = 0;

const AssignValueToProp = (name, val) => {

    switch (name) {
        case "forward":
            forward += val
            depth = aim > 0 ? depth + (aim * val) : depth
            break;
        case "down":
            down += val
            aim += val
            break;
        case "up":
            down -= val
            aim -= val
            break;
    }
}

for (let i = 0; i < data.length; i++) {
    let[name, val] = data[i].split(" ");
    

    AssignValueToProp(name, parseInt(val));
}

console.log(`Part 1 answer: ${forward * down}`);
console.log(`Part 2 answer: ${forward * depth}`);

