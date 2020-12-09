import dia08.*
import java.io.File
import java.lang.Math.abs
import java.util.*

object Dia08 {

    private fun parseInstruction(line: String): Instruction {
        val name = line.substring(0, 3)
        val value = Integer.parseInt(line.substring(4))

        return when (name) {
            "acc" -> ACC(value)
            "jmp" -> JMP(value)
            "nop" -> NOP(value)
            else -> throw Error("Instruccion incorrecta")
        }
    }

    fun puzzle(filePath: String) {
        val file = File(filePath)
        val sc = Scanner(file)

        val list = DoublyLinkedList<Instruction>()

        while (sc.hasNext()) {
            list.insert(parseInstruction(sc.nextLine()))
        }

        val computer = Computer(list)
        computer.runPuzzle2()
    }
}
