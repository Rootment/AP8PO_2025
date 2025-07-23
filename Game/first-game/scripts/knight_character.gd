extends CharacterBody2D


const SPEED = 200.0
const JUMP_VELOCITY = -375.0

var is_dead = false

@onready var animated_sprite = $AnimatedSprite2D

func _physics_process(delta):
	if is_dead:
		velocity = Vector2.ZERO
		return
	var direction := Input.get_axis("ui_left", "ui_right")
	velocity += get_gravity() * delta
	# Add the gravity.
	if is_on_floor():
		if not get_parent().game_running:
			$AnimatedSprite2D.play("idle")
		else:
			$RunCol.disabled=false
			# Handle jump.
			if Input.is_action_pressed("ui_accept"):
				$AnimatedSprite2D.play("jump")
				velocity.y = JUMP_VELOCITY
			elif Input.is_action_pressed("ui_down"):
				$AnimatedSprite2D.play("roll")
				$RunCol.disabled=true
			else :
				$AnimatedSprite2D.play("run")
				animated_sprite.flip_h = false
	else:
		$AnimatedSprite2D.play("jump")
		if Input.is_action_pressed("ui_down"):
			velocity.y += 20
			
	if direction > 0:
		animated_sprite.flip_h = false
	elif direction < 0:
		animated_sprite.flip_h = true
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
	move_and_slide()
	
func die():
	is_dead = true
	$AnimatedSprite2D.play("death")
	print("Player died!")
