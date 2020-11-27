import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { IngredientComponent } from './ingredient/ingredient.component';

const routes: Routes = [
  { path: 'home', component: AppComponent },
  { path: 'ingredient', component: IngredientComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
