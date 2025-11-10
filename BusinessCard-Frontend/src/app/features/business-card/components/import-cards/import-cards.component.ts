import { Component } from '@angular/core';
import {
  BusinessCardService,
  NewBusinessCard,
} from '../../services/business-card.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../../shared/shared.module';

type PreviewCard = NewBusinessCard & {
  importState: 'pending' | 'importing' | 'imported';
};

@Component({
  selector: 'app-import-cards',
  templateUrl: './import-cards.component.html',
  styleUrls: ['./import-cards.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule, SharedModule],
})
export class ImportCardsComponent {
  previewCards: PreviewCard[] = [];
  error: string | null = null;

  isLoading = false;
  isPreviewing = false;

  constructor(
    private cardService: BusinessCardService,
    private router: Router
  ) {}

  onFileSelected(event: Event | FileList): void {
    let files: FileList | null = null;

    if (event instanceof FileList) {
      files = event;
    } else {
      files = (event.target as HTMLInputElement)?.files;
    }

    const file = files?.[0];
    if (!file) return;

    this.isLoading = true;
    this.isPreviewing = false;
    this.error = null;
    this.previewCards = [];
    const fileExtension = file.name.split('.')[file.name.split('.').length - 1];
    if (fileExtension === 'csv' || fileExtension === 'xml') {
      this.cardService.previewImport(file).subscribe({
        next: (parsedCards) => {
          if (parsedCards.length === 0) {
            this.error = 'File is empty or in the wrong format.';
            this.isLoading = false;
            return;
          }

          this.previewCards = parsedCards.map((card) => ({
            ...card,
            importState: 'pending',
          }));

          this.isLoading = false;
          this.isPreviewing = true;
        },
        error: (err) => {
          console.error(err);
          this.error = 'Failed to parse file. Check console for details.';
          this.isLoading = false;
        },
      });
    } else {
      debugger;

      this.cardService.previewImportQR(file).subscribe({
        next: (parsedCard) => {
          if (parsedCard == null) {
            this.error = 'File is empty or in the wrong format.';
            this.isLoading = false;
            return;
          }

          this.previewCards = [
            {
              ...parsedCard,
              importState: 'pending',
            },
          ];

          this.isLoading = false;
          this.isPreviewing = true;
        },
        error: (err) => {
          console.error(err);
          this.error = 'Failed to parse file. Check console for details.';
          this.isLoading = false;
        },
      });
    }
  }

  onImportRow(card: PreviewCard): void {
    if (card.importState !== 'pending') {
      return;
    }

    card.importState = 'importing';
    this.error = null;

    this.cardService.createBusinessCard(card as any).subscribe({
      next: () => {
        card.importState = 'imported';
      },
      error: (err) => {
        console.error(err);
        this.error = `Failed to import "${card.name}". Please try again.`;
        card.importState = 'pending';
      },
    });
  }

  onCancel(): void {
    this.isPreviewing = false;
    this.previewCards = [];
    this.error = null;
  }
}
