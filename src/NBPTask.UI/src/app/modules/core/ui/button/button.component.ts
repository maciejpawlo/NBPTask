import { Component, input, output } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {

  title = input.required<string>();
  color = input<"default" | "primary" | "accent" | "warn">("default");
  click = output<MouseEvent>();
  type = input<"button" | "submit">("button");

  handleOnClick($event: MouseEvent): void {
    this.click.emit($event);
  }
}
