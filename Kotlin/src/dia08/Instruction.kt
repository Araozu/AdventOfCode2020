package dia08

sealed class Instruction(val value: Int, var counter: Int = 0) {
    fun visit() {
        counter++
    }

    fun resetCounter() {
        counter = 0
    }
}

class NOP(value: Int) : Instruction(value)
class ACC(value: Int) : Instruction(value)
class JMP(value: Int) : Instruction(value)
