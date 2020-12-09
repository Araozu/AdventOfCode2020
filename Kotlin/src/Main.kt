
fun getFilePath(day: Int, isTest: Boolean = false): String {
    val dayF = if (day.toString().length == 1) "0$day" else day.toString()

    return if (isTest) "/home/araozu/Programacion/AdventOfCode2020/inputs/input_${dayF}_test.txt"
    else "/home/araozu/Programacion/AdventOfCode2020/inputs/input_$dayF.txt"
}

fun benchmark(f: () -> Unit) {
    val tInicio = System.currentTimeMillis()
    f()
    val tFinal = System.currentTimeMillis()
    println("Milliseconds: ${tFinal - tInicio}")
}

fun main() {
    // benchmark { puzzle(getFilePath(7)) }
    benchmark { Dia08.puzzle(getFilePath(8)) }
}
