tapesize 20
alphabet
  : 0
  : 1
  : "PALINDROM"
  : "NORMAL"

input
    : 1
    : 0
    : 0
    : 0
    : 0
    : 0
    : 1
    : 0
    : 0
    : 0
    : 0
    : 0
    : 0
    : 0
    : 1

states
  : State 0
    : 0 "" > 1
    : 1 "" > 3
    : "" "PALINDROM" . -1

  : State 1
    : 0 0 > 1
    : 1 1 > 1
    : "" "" < 2

  : State 2
    : 0 "" < 5
    : 1 "" < 6
    : "" "" . 5

  : State 3
    : 0 0 > 3
    : 1 1 > 3
    : "" "" < 4

  : State 4
    : 0 "" < 6
    : 1 "" < 5
    : "" "" . 5

  : State 5
    : 0 0 < 5
    : 1 1 < 5
    : "" "" > 0

  : State 6
    : 0 "" < 6
    : 1 "" < 6
    : "" "NORMAL" . -1
