import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessCardDashboardComponent } from './components/business-card-dashboard/business-card-dashboard.component';
import { CardListComponent } from './components/card-list/card-list.component';
import { AddCardComponent } from './components/add-card/add-card.component';
import { ImportCardsComponent } from './components/import-cards/import-cards.component';
const routes: Routes = [
  {
    path: '',
    component: BusinessCardDashboardComponent,
    children: [
      { path: '', component: CardListComponent },
      { path: 'new', component: AddCardComponent },
      { path: 'import', component: ImportCardsComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BusinessCardRoutingModule {}
