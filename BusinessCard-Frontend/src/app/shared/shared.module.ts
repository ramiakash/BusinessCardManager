import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DragDropDirective } from './directives/drag-drop.directive';

@NgModule({
  declarations: [],
  imports: [CommonModule, DragDropDirective],
  exports: [DragDropDirective],
})
export class SharedModule {}
