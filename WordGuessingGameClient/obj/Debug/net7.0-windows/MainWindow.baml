   M S B A M L   `   `   `  ����  WordGuessingGameClient!    WordGuessingGame.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Word Guessing Game��5   	   $	��450��6$   $	��800��61   .��5      � .+�5   
   �� $��IP Address:��6   $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T   �� 5   
   -   5         ��Name$ txtIPAddress��5      $
��Left=�6(   $��	90,10,0,0q�6C   $	��120��6V   $	��Top=�6b   �� 5
   
   $��Port:��6   $
��Left=�6    $��
220,10,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $ txtPort��5      $
��Left=�6#   $��
260,10,0,0q�6>   $��60��6R   $	��Top=�6]   �� 5   
   $��	Username:��6   $
��Left=�6$   $��
330,10,0,0q�6?   $	��Top=�6S   �� 5   
   -   5        $ txtUserName��5      $
��Left=�6'   $��
400,10,0,0q�6B   $	��120��6V   $	��Top=�6b   �� 5   
   $��Time Limit:��6   $
��Left=�6&   $��
530,10,0,0q�6A   $	��Top=�6U   �� 5   
   -   5        $ txtTimeLimit��5      $
��Left=�6(   $��
610,10,0,0q�6C   $��60��6W   $	��Top=�6b   �� 5   
   -   5         ��Name$ 
btnConnect��5      $��Connect��6%   $
��Left=�67   $��
680,10,0,0q�6R   $	��100��6f   $	��Top=�6r   6�   �� 5   
   -   5        $ txtGuess��5      $
��Left=�6$   $��	10,40,0,0q�6?   $	��300��6R   $	��Top=�6^   ${�False��6v   �� 5   
   -   5        $ btnGuess��5      $��Guess��6#   $
��Left=�63   $��
320,40,0,0q�6N   $	��100��6b   $	��Top=�6n   ${�False��6�   �� 5   
   -   5        $ 
btnEndGame��5      $��End Game��6%   $
��Left=�68   $��
430,40,0,0q�6S   $	��100��6g   $	��Top=�6s   ${�False��6�   �� 5   
   -	   5        $ 	btnReplay��5      $��Replay��6$   $
��Left=�65   $��
540,40,0,0q�6P   $	��100��6d   $	��Top=�6p   ${�False��6�   �� 5   
   -
   5         ��Name$ lblTimeRemaining��5      $��Time Remaining: 00:00��6*   $
��Left=�6J   $��
650,40,0,0q�6e   $	��Top=�6y   �� 5!   
   -   5         ��Name$ txtGameInfo��5!      $��10,70,10,10q�6)   $	��Top=�6>   $
��Left=�6V   $
��Wrap=�6q   
�������6�   5"      5#      