using System;
using System.Numerics;

namespace Application
{
	public class Test
	{
//        Настройка анимаций — важный этап, который сделает твоего персонажа живым и отзывчивым.Сейчас я помогу тебе во всем разобраться. Действовать будем по плану: создадим все анимации, соберем их в машину состояний (Animator Controller), а затем «оживим» с помощью простого скрипта на C#.

//👣 Шаг 1: Подготовка и создание анимаций

//Прежде всего нужно добавить на персонажа компонент Animator и подготовить Animation Clips.

//1. Создаем Animator Controller: Выдели своего персонажа.В меню выбери Window > Animation > Animation, чтобы открыть окно анимации. Нажми Create, сохрани новый контроллер с любым именем (например, PlayerAnimator) в удобную папку проекту.Unity автоматически добавит на персонажа компонент Animator и Animator Controller будет создан.
//2. Добавляем клипы: В окне Animation для каждой нужной анимации создавай отдельный клип:
//   · Idle (покой)
//   · Run/Walk (ходьба/бег)
//   · Jump(прыжок)
//   · Attack(атака)
//   · Die(смерть)
//     Для этого в выпадающем меню слева выбери<Create New Clip...>.
//3. Настраиваем клипы: Когда клип создан, перетащи в таймлайн все спрайты этой анимации.Чтобы скорость анимации не была слишком высокой, открой контекстное меню в окне Animation(три точки) и включи Show Sample Rate.Уменьши значение Samples до 12, если ты используешь покадровую анимацию.

//⚙️ Шаг 2: Настройка машины состояний (Animator Controller)

//Теперь откроем окно настройки логики анимаций: Window > Animation > Animator.Мы создадим переходы между состояниями, которыми будет управлять код. Ключевой инструмент здесь — параметры, которые передают сигналы из скрипта в Animator.

//В окне Parameters создай:

//· isWalking (или Speed) — Float для плавного перехода между Idle и Run.
//· isGrounded — Bool для проверки, на земле ли персонаж.
//· isAttacking и isDead — Trigger.Этот тип параметра используйте для анимации, которая должна выполниться один раз и перезапустить себя.

//Настрой переходы между состояниями (дважды кликни на стрелку или создай новую, кликнув правой кнопкой мыши по состоянию):

//· Any State -> Die: Условие — isDead true.
//· Idle -> Run: Speed Greater 0.1.
//· Run -> Idle: Speed Less 0.1.
//· Any State -> Attack: Условие — isAttacking.Важно: сними галочку Has Exit Time, чтобы анимация прерывалась мгновенно.
//· Idle/Run -> Jump: Условие — isGrounded false. Чтобы не было резкого рывка, отметь Has Exit Time.
//· **Jump -> Idle/Run**: Условие — isGrounded true`.

//📄 Шаг 3: Скрипт управления анимациями

//Если у тебя еще нет скрипта для управления персонажем, создай его — например, PlayerController.cs.В нем мы опишем, как именно наши анимации будут откликаться на игру.

//```csharp
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//    {
        // --- Перетащите сюда компоненты в инспекторе ---
        [SerializeField] private Rigidbody2D rb; // Компонент Rigidbody2D
        [SerializeField] private Animator anim;  // Компонент Animator
        [SerializeField] private Transform groundCheck; // Пустой объект у ног для проверки земли
        [SerializeField] private LayerMask groundLayer; // Слой для земли

        [Header("Настройки движения")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;

        private float moveInput;
        private bool isGrounded;

        void Update()
        {
            // 1. Получаем нажатия клавиш
            moveInput = Input.GetAxisRaw("Horizontal");
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            // 2. --- Логика анимации ---
            // Передаем скорость бега (0 или 1) для blend tree или просто float
            anim.SetFloat("Speed", Mathf.Abs(moveInput));

            // Устанавливаем параметр isGrounded для перехода в анимацию прыжка/падения
            anim.SetBool("isGrounded", isGrounded);

            // Обработка атаки
            if (Input.GetButtonDown("Fire1")) // Левая кнопка мыши или Ctrl
            {
                anim.SetTrigger("isAttacking");
                // Здесь можете добавить код для нанесения урона
            }

            // Обработка прыжка
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                // Триггер jump, если у вас отдельная анимация прыжка
                // anim.SetTrigger("Jump");
            }
        }

        void FixedUpdate()
        {
            // 3. Движение персонажа
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // (Опционально) Поворот персонажа лицом к направлению движения
            if (moveInput != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            }
        }

        // Пример вызова смерти (можно вызвать из другого скрипта)
        public void Die()
        {
            anim.SetTrigger("isDead");
            // Отключить управление персонажем и т.д.
            this.enabled = false;
        }
    }
//```

//🖇️ Шаг 4: Финальная сборка

//1. Привяжи скрипт: Перетащи скрипт PlayerController на персонажа.
//2. Назначь ссылки: Перетащи компоненты персонажа(Rigidbody2D, Animator) в соответствующие поля[SerializeField] в инспекторе скрипта.
//3. Настрой прыжок: Создай пустой дочерний объект GroundCheck и помести его под ступни персонажа.Назначь слой Ground для платформ и выбери этот слой в groundLayer скрипта.
//4. Проверь параметры: Убедись, что имена параметров в аниматоре и скрипте совпадают.
//5. Помни об очередности: Если у тебя возникают проблемы с отображением анимации (Animator is not playing an AnimatorController), перепроверь, назначен ли контроллер в компоненте Animator и правильно ли ты получаешь ссылку GetComponent<Animator>() в скрипте.

//Надеюсь, это руководство поможет тебе в разработке твоей игры! Если возникнут сложности или понадобятся уточнения, спрашивай.
//	}
//}

