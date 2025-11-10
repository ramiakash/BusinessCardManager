import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CardFilters {
  name?: string;
  email?: string;
  phone?: string;
  gender?: string;
  dateOfBirth?: string;
}

export interface BusinessCard {
  id: string;
  name: string;
  gender: string;
  dateOfBirth: string;
  email: string;
  phone: string;
  address: string;
  photoBase64?: string;
}
export type NewBusinessCard = Omit<BusinessCard, 'id'>;

@Injectable({
  providedIn: 'root',
})
export class BusinessCardService {
  private readonly apiUrl = 'https://localhost:44319/api/BusinessCards';

  constructor(private http: HttpClient) {}

  getBusinessCards(filters?: CardFilters): Observable<BusinessCard[]> {
    let params = new HttpParams();

    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) {
          params = params.append(key, value);
        }
      });
    }

    return this.http.get<BusinessCard[]>(this.apiUrl, { params });
  }
  exportCards(format: 'csv' | 'xml', filters?: CardFilters): Observable<Blob> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) {
          params = params.append(key, value);
        }
      });
    }

    return this.http.get(`${this.apiUrl}/export/${format}`, {
      params,
      responseType: 'blob',
    });
  }

  getBusinessCardById(id: string): Observable<BusinessCard> {
    return this.http.get<BusinessCard>(`${this.apiUrl}/${id}`);
  }

  createBusinessCard(card: BusinessCard): Observable<BusinessCard> {
    return this.http.post<BusinessCard>(this.apiUrl, card);
  }

  deleteBusinessCard(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  previewImport(file: File): Observable<NewBusinessCard[]> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<NewBusinessCard[]>(`${this.apiUrl}/import`, formData);
  }
  previewImportQR(file: File): Observable<NewBusinessCard> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<NewBusinessCard>(
      `${this.apiUrl}/import/qr`,
      formData
    );
  }
}
