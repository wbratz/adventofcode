import json

class Items:
    def __init__(self, forward, down, depth, aim):
        self.forward = forward
        self.down = down
        self.depth = depth
        self.aim = aim

    def get_forward(self):
        return self.forward
    
    def get_down(self):
        return self.down

    def get_depth(self):
        return self.depth

    def get_aim(self):
        return self.aim

    def set_forward(self, value):
        self.forward = value

    def set_down(self, value):
        self.down = value

    def set_depth(self, value):
        self.depth = value

    def set_aim(self, value):
        self.aim = value

items = Items(0, 0, 0, 0)

def AssignValueToItems(name, value):
    if name == "forward":
        items.set_forward(items.get_forward() + int(value))
        if items.get_aim() > 0:
            items.set_depth(items.get_depth() + (items.get_aim() * int(value)))

    if name == "down":
        items.set_down(items.get_down() + int(value))
        items.set_aim(items.get_aim() + int(value))
    
    if name == "up":
        items.set_down(items.get_down() - int(value))
        items.set_aim(items.get_aim() - int(value))
    

jsonFile = open("../data.json")

data = json.load(jsonFile)


for item in data:
    currentItem = item.split()

    AssignValueToItems(currentItem[0], currentItem[1])

print(f"Part 1 answer: {items.get_forward() * items.get_down()}")
print(f"Part 2 answer: {items.get_forward() * items.get_depth()}")



