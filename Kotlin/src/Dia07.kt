import java.io.File
import java.util.*
import kotlin.collections.ArrayList
import kotlin.collections.HashMap

data class BagContent(val amount: Int, val color: String)

class Bag(val bags: ArrayList<BagContent>, var containsColor: Boolean = false) {
    fun doesContainBag() {
        containsColor = true
    }

    fun addBagContent(b: BagContent) {
        bags.add(b)
    }
}

val bags = HashMap<String, Bag>()
const val bagColor = "shiny gold"

fun parseLine(line: String): Pair<String, Bag> {
    val endColorPosition = line.indexOf(" bags")
    val bagColor = line.substring(0, endColorPosition)

    val currentBag = Bag(ArrayList())

    var restLine = line.substring(endColorPosition + 14)
    if (restLine == "no other bags.") return Pair(bagColor, currentBag)

    while (true) {
        val positionBagStr = restLine.indexOf(" bag")

        val number = restLine.substring(0, 1)
        val amount = Integer.parseInt(number)
        val color = restLine.substring(2, positionBagStr)

        val bagContent = BagContent(amount, color)
        currentBag.addBagContent(bagContent)

        val nextCommaPosition = restLine.indexOf(',')

        if (nextCommaPosition == -1) break

        restLine = restLine.substring(nextCommaPosition + 2)
    }

    return Pair(bagColor, currentBag)
}

fun isContainer(bag: Bag): Boolean {
    if (bag.containsColor) return true

    for ((_, color) in bag.bags) {
        // If the current bag can hold the desired color
        if (color == bagColor) {
            bag.doesContainBag()
            return true
        }

        // else, search recursively in the other bags
        val newSearchBag = bags[color]
        if (newSearchBag != null && isContainer(newSearchBag)) {
            bag.doesContainBag()
            return true
        }

    }

    return false
}

fun countInnerBags(bag: Bag): Int {
    var amount = 0

    for ((bagAmount, color) in bag.bags) {
        val newSearchBag = bags[color] ?: continue

        amount += bagAmount + bagAmount * countInnerBags(newSearchBag)
    }

    return amount
}

fun puzzle(filePath: String) {
    val file = File(filePath)
    val sc = Scanner(file)

    while (sc.hasNext()) {
        val line = sc.nextLine()

        val (bagColor, bag) = parseLine(line)
        bags[bagColor] = bag
    }

    var bagContainersAmount = 0
    for ((_, bag) in bags) {
        if (isContainer(bag)) bagContainersAmount++
    }

    println("Bag amount: $bagContainersAmount")

    val shinyGoldBag = bags[bagColor] ?: return
    println("Bag count: ${countInnerBags(shinyGoldBag)}")
}
