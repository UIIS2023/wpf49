   M S B A M L   `   `   `  ����D  @pekaraWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:pekaraWPF.FormepekaraWPF.Forme  5   	   #    pekaraWPF.Forme.FrmPorudzbine   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:pekaraWPF.Forme     5   	     � Title$  FrmPorudzbine��5   	   $��442.623��6   $��422.541��60   .��5	      � .+�5
   
   �� -   5         ��Name$ lblZaposleni��5
      $��
Zaposleni:��6&   $
��Left=�6;   $��	37,41,0,0q�6V   $	��Top=�6i   �� 5   
   -   5        $ lblKupac��5      $��Kupac:��6"   $
��Left=�63   $��	37,72,0,0q�6N   $	��Top=�6a    J� RenderTransformOrigin$ -3.935,-2.789&�6y   �� 5   
   -   5        $ 	lblPecivo��5      $��Pecivo:��6#   $
��Left=�65   $��
37,103,0,0q�6P   $	��Top=�6d   �� 5   
   -   5        $ lblPice��5      $��Pice:��6!   $
��Left=�61   $��
37,134,0,0q�6L   $	��Top=�6`   �� 5   
   -   5        $ lblTip��5      $��Tip Porudzbine:��6    $
��Left=�6:   $��
37,165,0,0q�6U   $	��Top=�6i   �� 5   
   -   5        $ lblCena��5      $��Cena:��6!   $
��Left=�61   $��
37,196,0,0q�6L   $	��Top=�6`   �� 5   
   -   5        $ lblVreme��5      $��Vreme:��6"   $
��Left=�63   $��
37,227,0,0q�6N   $	��Top=�6b   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��39��6R   $��
23,357,0,0q�6^   $	��Top=�6r   $	��114��6�   6�   �� 5   
   -	   5        $ 	btnOtkazi��5      $��Otkazi��6$   $
��Left=�65   $��39��6P   $��247,357,0,0q�6\   $	��Top=�6q   $	��120��6�   6�   �� 5   
   -
   5         ��Name$ txtCena��5      $
��Left=�6#   $��23��6>   $��143,199,0,0q�6J    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   �� 5   
   -   5        $ txtVreme��5      $
��Left=�6$   $��23��6?   $��143,230,0,0q�6K   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��120��6�   �� 5   
   -   5         ��Name$ cbTip��5      $
��Left=�6"   $��143,169,0,0q�6=   $	��Top=�6R   $	��120��6j    �� DisplayMemberPath$ 	nazivTipa��6v    �� SelectedValuePath$ tipPorudzbineID��6�   �� 5   
   -   5        $ cbZaposleni��5      $
��Left=�6(   $��
143,45,0,0q�6C   $	��Top=�6W   $	��120��6o   $	 ime��6{   $ zaposleniID��6�   �� 5   
   -   5        $ cbKupac��5      $
��Left=�6$   $��
143,76,0,0q�6?   $	��Top=�6S   $	��120��6k   $	 ime��6w   $ kupacID��6�   �� 5   
   -   5        $ cbPecivo��5      $
��Left=�6%   $��143,107,0,0q�6@   $	��Top=�6U   $	��120��6m   $	 ime��6y   $ pecivoID��6�   �� 5   
   -   5        $ cbPice��5      $
��Left=�6#   $��143,138,0,0q�6>   $	��Top=�6S   $	��120��6k   $	 ime��6w   $ piceID��6�   5      5      