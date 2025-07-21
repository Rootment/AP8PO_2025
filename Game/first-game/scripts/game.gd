extends Node2D

const CHAR_START_POS := Vector2i(80,160)
const CAM_START_POS := Vector2i(165,100)

var speed : float
const START_SPEED : float = 3.5
const MAX_SPEED : int = 20
var screen_size : Vector2i

# Called when the node enters the scene tree for the first time.
func _ready():
	screen_size = get_window().size
	new_game()

func new_game():
	$Knight.position = CHAR_START_POS
	$Knight.velocity = Vector2i(0,0)
	$Camera2D.position = CAM_START_POS
	$Ground.position = Vector2i(0,0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	speed = START_SPEED
	
	$Knight.position.x += speed
	$Camera2D.position.x += speed
	
	if $Camera2D.position.x - $Ground.position.x > screen_size.x * 1.5:
		$Ground.position.x += screen_size.x
