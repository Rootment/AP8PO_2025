extends Node2D

var tree = preload("res://scenes/tree_obstacle.tscn")
var wall = preload("res://scenes/wall_obstacle.tscn")
var crate = preload("res://scenes/create_obstacle.tscn")
var slime = preload("res://scenes/slime_obstacle.tscn")
var obstacles_types:=[tree,wall,crate]
var obstacles:Array
var bird_heights:=[200,390]

const CHAR_START_POS := Vector2i(80,160)
const CAM_START_POS := Vector2i(165,100)
var highest_score : int
var score : int
const SCORE_MODIFIER : int = 10
var speed : float
const START_SPEED : float = 3.5
const MAX_SPEED : int = 20
const SPEED_MODIFIER : int = 5000
var screen_size : Vector2i
const GROUND_HEIGHT : int = 168.25
var game_running : bool
var last_obs

# Called when the node enters the scene tree for the first time.
func _ready():
	screen_size = get_window().size
	new_game()

func new_game():
	score = 0
	show_score()
	$Knight.position = CHAR_START_POS
	$Knight.velocity = Vector2i(0,0)
	$Camera2D.position = CAM_START_POS
	$Ground.position = Vector2i(0,0)
	
	$HUD.get_node("StartLabel").show()
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if game_running:
		speed = START_SPEED + score / SPEED_MODIFIER
		if speed > MAX_SPEED:
			speed = MAX_SPEED
		
		generate_obs()
		
		$Knight.position.x += speed
		$Camera2D.position.x += speed
		
		var half_screen = screen_size * 0.137
		var left_edge = $Camera2D.position.x - half_screen.x
		var right_edge = $Camera2D.position.x + half_screen.x
		# Clamping pouze horizontálně (vertikálně, pokud chceš)
		$Knight.position.x = clamp($Knight.position.x, left_edge, right_edge)
		
		score += speed
		show_score()
		
		if $Camera2D.position.x - $Ground.position.x > screen_size.x * 1.5:
			$Ground.position.x += screen_size.x
	else:
		var half_screen = screen_size * 0.137
		var left_edge = $Camera2D.position.x - half_screen.x
		var right_edge = $Camera2D.position.x + half_screen.x
		# Clamping pouze horizontálně (vertikálně, pokud chceš)
		$Knight.position.x = clamp($Knight.position.x, left_edge, right_edge)
		if Input.is_action_pressed("ui_accept"):
			game_running=true
			$HUD.get_node("StartLabel").hide()

func show_score():
	$HUD.get_node("ScoreLabel").text = "SCORE: " + str(score/SCORE_MODIFIER)
	
	if score > highest_score:
		highest_score = score/SCORE_MODIFIER
		
	$HUD.get_node("HighScore").text = "HIGHEST SCORE: " + str(highest_score)
	
func generate_obs():
	if obstacles.is_empty() or last_obs.position.x < score + randi_range(100,300):
		var obs_type = obstacles_types[randi()%obstacles_types.size()]
		var obs
		var max_obs = 3
		for i in range(randi()%max_obs+1):
			obs = obs_type.instantiate()
			var obs_x: int = screen_size.x + score + 100 + (i*100)*speed
			# fixed generating axis y -> taky je každy jinak velký
			var obs_y: int = GROUND_HEIGHT - 60
			last_obs = obs
			add_obs(obs,obs_x,obs_y)
		
func add_obs(obs,x,y):
	obs.position = Vector2i(x,y)
	add_child(obs)
	obstacles.append(obs)
