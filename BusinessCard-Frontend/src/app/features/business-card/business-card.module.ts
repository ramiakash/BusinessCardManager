import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { BusinessCardRoutingModule } from './business-card-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { BusinessCardComponent } from './business-card.component';
import { BusinessCardDashboardComponent } from './components/business-card-dashboard/business-card-dashboard.component';

import { CardListComponent } from './components/card-list/card-list.component';
import { AddCardComponent } from './components/add-card/add-card.component';
import { ImportCardsComponent } from './components/import-cards/import-cards.component';

@NgModule({
  declarations: [BusinessCardComponent, BusinessCardDashboardComponent],
  imports: [
    CommonModule,
    BusinessCardRoutingModule,
    ReactiveFormsModule,
    SharedModule,

    CardListComponent,
    AddCardComponent,
    ImportCardsComponent,
  ],
})
export class BusinessCardModule {}
