import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-icon-avatar',
  imports: [],
  templateUrl: './icon-avatar.html',
  styleUrl: './icon-avatar.css',
})
export class IconAvatar {
  @Input() cor: string = '#000000';
  @Input() icone: string = 'ri-question-line';
}
