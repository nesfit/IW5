import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { RecipeService } from '../api/services';
import { EnumToArrayPipe } from '../enum-to-array.pipe';

import { RecipeComponent } from './recipe.component';

describe('RecipeComponent', () => {
  let component: RecipeComponent;
  let fixture: ComponentFixture<RecipeComponent>;

  const recipeService = jasmine.createSpyObj('RecipeService', ['recipeGetById']);

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, FormsModule],
      declarations: [RecipeComponent, EnumToArrayPipe],
      providers: [
        { provide: RecipeService, useValue: recipeService },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
