import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { BusinessCardService } from '../../services/business-card.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class AddCardComponent implements OnInit {
  businessCardForm!: FormGroup;

  photoPreview: string | ArrayBuffer | null = null;

  constructor(
    private fb: FormBuilder,
    private cardService: BusinessCardService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.businessCardForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      gender: ['Male', [Validators.required]],
      address: [''],
      photoBase64: [null],
    });
  }

  get f() {
    return this.businessCardForm.controls;
  }

  onSubmit(): void {
    if (this.businessCardForm.invalid) {
      this.businessCardForm.markAllAsTouched();
      return;
    }

    this.cardService.createBusinessCard(this.businessCardForm.value).subscribe({
      next: () => {
        alert('Business Card Created!');
        this.router.navigate(['/cards']);
      },
      error: (err) => {
        console.error('Failed to create card:', err);
      },
    });
  }

  onFileChange(event: Event): void {
    const file = (event.target as HTMLInputElement)?.files?.[0];
    if (!file) {
      return;
    }

    if (file.size > 1048576) {
      alert('File is too large! Max 1MB.');
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      const result = reader.result as string;
      this.photoPreview = result;

      this.businessCardForm.patchValue({ photoBase64: result.split(',')[1] });
    };
    reader.readAsDataURL(file);
  }
}
