import java.io.File
import java.util.*
import kotlin.collections.ArrayList

object Dia09 {

    private fun getSmallestLargest(numbers: ArrayList<Long>) {
        val max = numbers.reduce { acc, l -> if (acc > l) acc else l }
        val min = numbers.reduce { acc, l -> if (acc < l) acc else l }

        println("The sum of the smallest and largest number of the sequence is: ${max + min}")
    }

    private fun findSecuence(numbers: ArrayList<Long>, value: Long) {
        for (i in 0 until numbers.size) {
            var currentValue = numbers[i]
            val fnumbers = kotlin.collections.ArrayList<Long>()
            fnumbers.add(currentValue)

            for (j in i+1 until numbers.size) {
                currentValue += numbers[j]
                fnumbers.add(numbers[j])

                if (currentValue == value) {
                    getSmallestLargest(fnumbers)
                    return
                } else if (currentValue > value) {
                    break
                }

            }
        }
    }

    private fun isValid(numbers: ArrayList<Long>, pos: Int, minPos: Int): Boolean {
        val testNumber = numbers[pos]

        var i = minPos
        while (i < minPos + 25) {
            var j = i + 1

            while (j < minPos + 25) {
                val first = numbers[i]
                val second = numbers[j]
                if (first + second == testNumber) return true
                j++
            }

            i++
        }

        return false
    }

    fun puzzle(filePath: String) {
        val file = File(filePath)
        val sc = Scanner(file)

        val numbers = ArrayList<Long>()
        while (sc.hasNextLine()) {
            val line = sc.nextLine()
            numbers.add(line.toLong())
        }

        for ((minPos, i) in (25 until numbers.size).withIndex()) {
            if (!isValid(numbers, i, minPos)) {
                val invalidNumber = numbers[i]

                println("The first invalid number is $invalidNumber")

                findSecuence(numbers, invalidNumber)

                return
            }
        }

    }

}