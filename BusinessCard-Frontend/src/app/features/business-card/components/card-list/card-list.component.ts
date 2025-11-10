import { Component, OnInit } from '@angular/core';
import {
  BusinessCard,
  BusinessCardService,
  CardFilters,
} from '../../services/business-card.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {
  Observable,
  BehaviorSubject,
  switchMap,
  debounceTime,
  startWith,
} from 'rxjs';
import { saveAs } from 'file-saver';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
})
export class CardListComponent implements OnInit {
  cards$!: Observable<BusinessCard[]>;
  filterForm: FormGroup;

  private refreshTrigger = new BehaviorSubject<void>(undefined);

  constructor(
    private cardService: BusinessCardService,
    private fb: FormBuilder
  ) {
    this.filterForm = this.fb.group({
      name: [''],
      email: [''],
      phone: [''],
      gender: [''],
      dateOfBirth: [null],
    });
  }

  ngOnInit(): void {
    const filter$ = this.filterForm.valueChanges.pipe(
      startWith(this.filterForm.value),
      debounceTime(300)
    );

    this.cards$ = this.refreshTrigger.pipe(
      switchMap(() => filter$),

      switchMap((filters: CardFilters) =>
        this.cardService.getBusinessCards(this.cleanFilters(filters))
      )
    );
  }

  private cleanFilters(filters: CardFilters): CardFilters {
    return Object.entries(filters).reduce((acc, [key, value]) => {
      if (value) {
        (acc as any)[key] = value;
      }
      return acc;
    }, {} as CardFilters);
  }

  onDelete(id: string): void {
    if (!confirm('Are you sure?')) return;

    this.cardService.deleteBusinessCard(id).subscribe({
      next: () => {
        alert('Card deleted');

        this.refreshTrigger.next();
      },
      error: (err) => console.error('Failed to delete card:', err),
    });
  }

  onExport(format: 'csv' | 'xml'): void {
    const filters = this.cleanFilters(this.filterForm.value);

    this.cardService.exportCards(format, filters).subscribe({
      next: (blob) => {
        const fileName = `business_cards_${new Date().toISOString()}.${format}`;
        saveAs(blob, fileName);
      },
      error: (err) => console.error('Export failed:', err),
    });
  }

  onResetFilters(): void {
    this.filterForm.reset();
  }
}
