extends CharacterBody2D


const SPEED = 200.0
const JUMP_VELOCITY = -400.0

@onready var animated_sprite = $AnimatedSprite2D

func _physics_process(delta):
	var direction := Input.get_axis("move_left", "move_right")
	velocity += get_gravity() * delta
	# Add the gravity.
	if is_on_floor():
		if not get_parent().game_running:
			$AnimatedSprite2D.play("idle")
		else:
			$RunCol.disabled=false
			# Handle jump.
			if Input.is_action_pressed("jump"):
				$Jump_sound.play()
				$AnimatedSprite2D.play("jump")
				velocity.y = JUMP_VELOCITY
			elif Input.is_action_pressed("roll"):
				$AnimatedSprite2D.play("roll")
				$RunCol.disabled=true
			else :
				$AnimatedSprite2D.play("run")
				animated_sprite.flip_h = false
	else:
		if Input.is_action_pressed("roll"):
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
