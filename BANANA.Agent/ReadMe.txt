* 바나나 에이전트 프로그램 설정 및 FAQ


1) The specified module could not be found 오류 해결 방법
   프로젝트 속성 > Debug > Enable the Visual Studio hostring process 체크박스가 체크 해제되어 있어야 합니다.
   이 속성이 체크되어 있으면, HookManager.Callbacks.cs의 222번째 라인에서 SetWindowsHookEx 함수의 반환 값이 항상 0으로 떨어집니다.
   SetWindowsHookEx 함수의 반환 값이 0으로 떨어지면, 후킹이 제대로 안 된 상태이고 오류가 발생하게 됩니다.